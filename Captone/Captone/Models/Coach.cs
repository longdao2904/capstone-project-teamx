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
    
    public partial class Coach
    {
        public Coach()
        {
            this.Schedules = new HashSet<Schedule>();
            this.Trips = new HashSet<Trip>();
            this.Schedules1 = new HashSet<Schedule>();
        }
    
        public int CoachID { get; set; }
        public string NumberPlate { get; set; }
        public Nullable<int> RouteID { get; set; }
        public int CoachTypeID { get; set; }
    
        public virtual CoachType CoachType { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
        public virtual ICollection<Schedule> Schedules1 { get; set; }
    }
}