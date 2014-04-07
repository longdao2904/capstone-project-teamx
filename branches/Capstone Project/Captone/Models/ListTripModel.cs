﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Captone.Models
{
    public class ListTripModel
    {
        public string RouteName { get; set; }
        public string NumberPlate { get; set; }
        public System.TimeSpan EstimateArrivalTime { get; set; }
        public System.TimeSpan EstimateDepartureTime { get; set; }
        public int TripID { get; set; }
        public string Status { get; set; }
        public int StopStation { get; set; }
        public int RequestID { get; set; }
        public int IndicateOrder { get; set; }
        public int ToLocation { get; set; }
        public DateTime Date { get; set; }
        public string StopStationName { get; set; }
    }
}