using odulsistemi.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using odulsistemi.ViewModel;

namespace odulsistemi.Controllers
{
    public class HomeController : Controller
    {
        odulSistemiEntities db = new odulSistemiEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(odul_user user)
        {
            try
            {
                using (odulSistemiEntities db = new odulSistemiEntities())
                {

                    var usr = db.odul_user.Where(u => u.kullanici_adi == user.kullanici_adi && u.sifre == user.sifre).FirstOrDefault();

                    if (usr.yetkiID == 1)
                    {
                        Session["UyeId"] = usr.user_id.ToString();
                        Session["KullaniciAdi"] = usr.kullanici_adi.ToString();
                        return RedirectToAction("ikinciAnasayfa");
                    }
                    else if (usr.yetkiID == 2)
                    {
                        Session["UyeId"] = usr.user_id.ToString();
                        Session["KullaniciAdi"] = usr.kullanici_adi.ToString();
                        return RedirectToAction("adminSayfa");
                    }
                    else
                    {
                        TempData["uyarıMesaj"] = "Kullanıcı Adı veya Şifre Hatalı!";
                    }
                }
                return View();
            }
            catch
            {

                TempData["uyarıMesaj"] = "Kullanıcı Adı veya Şifre Hatalı!";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["KullaniciAdi"] = null;
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KayitOl(odul_user model)
        {
            if (ModelState.IsValid)
            {
                odul_user kayit = new odul_user();
                var deneme = db.odul_user.Where(a => model.kullanici_adi == a.kullanici_adi && model.email == a.email);
                if (deneme.Count() <= 0)
                {
                    kayit.kullanici_adi = model.kullanici_adi;
                    kayit.sifre = model.sifre;
                    kayit.ad = model.ad;
                    kayit.soyad = model.soyad;
                    kayit.sicilNo = model.sicilNo;
                    kayit.unvan = model.unvan;
                    kayit.bolum = model.bolum;
                    kayit.email = model.email;
                    kayit.yetkiID = 1;
                    db.odul_user.Add(kayit);
                    db.SaveChanges();
                    TempData["KullaniciMesaji2"] = "Başarılı bir şekilde kaydedilmiştir.";
                }
                else
                {
                    TempData["KullaniciMesaji"] = "Kullanıcı adı vaya mail önceden alınmış!";
                }

            }
            return View();

        }

        public ActionResult FormDoldur()
        {

            ViewBag.odulTipi_id = new SelectList(db.odul_tipi, "odulTipi_id", "tip_adi");
            return View();
        }

        [HttpPost]
        public ActionResult FormDoldur(odul_oneri oneriModel)
        {
            var id = Convert.ToInt32(Session["UyeId"]);
            DateTime zaman = DateTime.Now;
            oneriModel.user_id = id;
            oneriModel.odul_tarih = zaman;
            oneriModel.status = 1;
            if (ModelState.IsValid)
            {
                db.odul_oneri.Add(oneriModel);
                db.SaveChanges();
            }
            return RedirectToAction("ikinciAnasayfa");
        }

        public ActionResult ikinciAnasayfa(int Page = 1)
        {
            var id = Convert.ToInt32(Session["UyeId"]);
            var uListe = db.odul_oneri.Where(m => m.user_id == id).OrderByDescending(m => m.odul_id).ToPagedList(Page, 10);

            return View(uListe);
        }

        public ActionResult adminSayfa(int Page = 1)
        {
            var makales1 = db.odul_oneri.Where(m => m.status == 1).OrderByDescending(m => m.odul_id).ToPagedList(Page, 10);
            return View(makales1);
        }

        public ActionResult adminLoginSayfa()
        {
            ViewBag.Message = "Admin Login Sayfası";
            return View();
        }

        public ActionResult goruntuleSayfa(int id)
        {
            var sayfa = db.odul_oneri.Where(m => m.odul_id == id).SingleOrDefault();
            if (sayfa == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.odul_tipi, "odulTipi_id", "tip_adi", sayfa.odul_id);
            return View(sayfa);
        }
        [HttpPost]
        public ActionResult kontrolSayfa(int id, odul_oneri oneri)
        {
            var onay = db.odul_oneri.Where(m => m.odul_id == id).SingleOrDefault();
            return View(onay);
        }

        public ActionResult kontrolSayfa(int id)
        {
            var kontrolSayfa = db.odul_oneri.Where(m => m.odul_id == id).SingleOrDefault();
            if (kontrolSayfa == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.odul_tipi, "odulTipi_id", "tip_adi", kontrolSayfa.odul_id);

            return View(kontrolSayfa);
        }

        public ActionResult onaylamaSayfasi(int? id)
        {
            var onayy = db.odul_oneri.Where(m => m.odul_id == id).SingleOrDefault();
            return View(onayy);
        }

        [HttpPost]
        public ActionResult onaylamaSayfasi(int? id, odul_oneri oneri, odul_kontrol kontrol)
        {
            var kontroller = db.odul_kontrol.Where(m => m.odul_id == id).SingleOrDefault();
            var oduller = db.odul_oneri.Where(m => m.odul_id == id).SingleOrDefault();
            odul_kontrol k = new odul_kontrol();
            try
            {
                if (kontrol.kabul_tarihi != null)
                {
                    k.kabul_tarihi = kontrol.kabul_tarihi;
                    k.uygulama_tarihi = kontrol.uygulama_tarihi;
                    k.durum = "Onaylandı";
                    k.odul_id = id;
                    oduller.status = 2;
                    k.user_id = Convert.ToInt32(Session["UyeId"]);
                    db.odul_kontrol.Add(k);
                }
                else
                {
                    k.red_nedeni = kontrol.red_nedeni;
                    k.red_tarihi = kontrol.red_tarihi;
                    k.durum = "Reddedildi";
                    oduller.status = 3;
                    k.odul_id = id;
                    k.user_id = Convert.ToInt32(Session["UyeId"]);
                    db.odul_kontrol.Add(k);
                }

                db.SaveChanges();
                return RedirectToAction("adminSayfa");
            }
            catch
            {
                ViewBag.kontroller = new SelectList(db.odul_kontrol, "kabul_tarihi", "uygulama_tarihi", kontroller.odul_id);
                return View(kontroller);
            }
        }


        public ActionResult reddetmeSayfasi(int id)
        {
            var red = db.odul_oneri.Where(m => m.odul_id == id).SingleOrDefault();
            return View(red);
        }
        [HttpPost]
        public ActionResult reddetmeSayfasi(int id, odul_oneri oneri, odul_kontrol kontrol)
        {
            var kontroller = db.odul_kontrol.Where(m => m.odul_id == id).SingleOrDefault();
            var reddet = db.odul_oneri.Where(m => m.odul_id == id).SingleOrDefault();
            odul_kontrol k = new odul_kontrol();
            try
            {
                if (kontrol.kabul_tarihi != null)
                {
                    k.kabul_tarihi = kontrol.kabul_tarihi;
                    k.uygulama_tarihi = kontrol.uygulama_tarihi;
                    k.durum = "Onaylandı";
                    k.odul_id = id;
                    reddet.status = 2;
                    k.user_id = Convert.ToInt32(Session["UyeId"]);
                    db.odul_kontrol.Add(k);
                }
                else
                {
                    k.red_nedeni = kontrol.red_nedeni;
                    k.red_tarihi = kontrol.red_tarihi;
                    k.durum = "Reddedildi";
                    k.odul_id = id;
                    reddet.status = 3;
                    k.user_id = Convert.ToInt32(Session["UyeId"]);
                    db.odul_kontrol.Add(k);
                }
                db.SaveChanges();
                return RedirectToAction("adminSayfa");
            }
            catch
            {
                ViewBag.kontroller = new SelectList(db.odul_kontrol, "red_nedeni", "red_tarihi", kontroller.odul_id);
                return View(kontrol);
            }
        }

        public ActionResult redSayfa(int Page = 1)
        {
            var redListe = db.odul_kontrol.Where(m => m.odul_oneri.status == 3).OrderByDescending(m => m.odul_id).ToPagedList(Page, 10);
            return View(redListe);
        }

        public ActionResult onaySayfa(int Page = 1)
        {
            var onayListe = db.odul_kontrol.Where(m => m.odul_oneri.status == 2).OrderByDescending(m => m.odul_id).ToPagedList(Page, 10);
            return View(onayListe);
        }
        [HttpPost]
        public ActionResult onaySayfa(int id, odul_oneri oneri, odul_kontrol kontrol)
        {
            return View();
        }

        public ActionResult kullaniciGoruntule(int id)
        {
            OdulKontrol sdmodel = new OdulKontrol();
            sdmodel.oneri = db.odul_oneri.Where(m => m.odul_id == id).SingleOrDefault();
            sdmodel.kontrol = db.odul_kontrol.Where(m => m.odul_id == id).SingleOrDefault();
            if (sdmodel == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.odul_tipi, "odulTipi_id", "tip_adi", sdmodel.oneri.odul_id);
            return View(sdmodel);
        }

        public ActionResult onayGoruntule(int id)
        {
            OdulKontrol sdmodel = new OdulKontrol();
            sdmodel.oneri = db.odul_oneri.Where(m => m.odul_id == id).SingleOrDefault();
            sdmodel.kontrol = db.odul_kontrol.Where(m => m.odul_id == id).SingleOrDefault();
            if (sdmodel == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.odul_tipi, "odulTipi_id", "tip_adi", sdmodel.oneri.odul_id);
            return View(sdmodel);
        }

        public ActionResult redGoruntule(int id)
        {
            OdulKontrol sdmodel = new OdulKontrol();
            sdmodel.oneri = db.odul_oneri.Where(m => m.odul_id == id).SingleOrDefault();
            sdmodel.kontrol = db.odul_kontrol.Where(m => m.odul_id == id).SingleOrDefault();
            if (sdmodel == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.odul_tipi, "odulTipi_id", "tip_adi", sdmodel.oneri.odul_id);
            return View(sdmodel);
        }

    }


}