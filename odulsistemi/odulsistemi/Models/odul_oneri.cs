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
    
    public partial class odul_oneri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public odul_oneri()
        {
            this.odul_kontrol = new HashSet<odul_kontrol>();
        }
    
        public int odul_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> odulTipi_id { get; set; }
        public string tanim { get; set; }
        public Nullable<System.DateTime> odul_tarih { get; set; }
        public string mevcut_durum { get; set; }
        public string yeni_durum { get; set; }
        public string fayda { get; set; }
        public Nullable<int> status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<odul_kontrol> odul_kontrol { get; set; }
        public virtual odul_tipi odul_tipi { get; set; }
        public virtual odul_user odul_user { get; set; }
    }
}