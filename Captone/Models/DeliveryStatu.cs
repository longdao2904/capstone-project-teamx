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
    
    public partial class DeliveryStatu
    {
        public DeliveryStatu()
        {
            this.Requests = new HashSet<Request>();
        }
    
        public int DeliveryStatusID { get; set; }
        public string StatusName { get; set; }
        public string Note { get; set; }
    
        public virtual ICollection<Request> Requests { get; set; }
    }
}