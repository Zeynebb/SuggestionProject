//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace odulsistemi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class odul_user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public odul_user()
        {
            this.odul_kontrol = new HashSet<odul_kontrol>();
            this.odul_oneri = new HashSet<odul_oneri>();
        }
    
        public int user_id { get; set; }
        public string kullanici_adi { get; set; }
        public string ad { get; set; }
        public string soyad { get; set; }
        public string sifre { get; set; }
        public string sicilNo { get; set; }
        public string unvan { get; set; }
        public string bolum { get; set; }
        public Nullable<int> yetkiID { get; set; }
        public string email { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<odul_kontrol> odul_kontrol { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<odul_oneri> odul_oneri { get; set; }
    }
}
