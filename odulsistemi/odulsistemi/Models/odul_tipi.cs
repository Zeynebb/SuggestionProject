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
    
    public partial class odul_tipi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public odul_tipi()
        {
            this.odul_oneri = new HashSet<odul_oneri>();
        }
    
        public int odulTipi_id { get; set; }
        public string tip_adi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<odul_oneri> odul_oneri { get; set; }
    }
}