//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Captone.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Province
    {
        public Province()
        {
            this.Stations = new HashSet<Station>();
        }
    
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
    
        public virtual ICollection<Station> Stations { get; set; }
    }
}