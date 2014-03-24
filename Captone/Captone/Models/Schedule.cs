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
    
    public partial class Schedule
    {
        public Schedule()
        {
            this.Trips = new HashSet<Trip>();
            this.Coaches = new HashSet<Coach>();
        }
    
        public int ScheduleID { get; set; }
        public int RouteID { get; set; }
        public int CoachID { get; set; }
        public System.TimeSpan EstimateDepartureTime { get; set; }
        public System.TimeSpan EstimateArrivalTime { get; set; }
        public Nullable<double> EstimateVolume { get; set; }
    
        public virtual Coach Coach { get; set; }
        public virtual Route Route { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
        public virtual ICollection<Coach> Coaches { get; set; }
    }
}
