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
    
    public partial class odul_kontrol
    {
        public int kontrol_id { get; set; }
        public Nullable<int> odul_id { get; set; }
        public string durum { get; set; }
        public string red_nedeni { get; set; }
        public Nullable<System.DateTime> red_tarihi { get; set; }
        public Nullable<System.DateTime> uygulama_tarihi { get; set; }
        public Nullable<System.DateTime> kabul_tarihi { get; set; }
        public Nullable<int> user_id { get; set; }
    
        public virtual odul_oneri odul_oneri { get; set; }
        public virtual odul_user odul_user { get; set; }
    }
}
