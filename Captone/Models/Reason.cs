using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Captone.Models
{
    public class Reason
    {
        public string RequestCode { get; set; }
        public string FromTo { get; set; }
        public int NumberOfWay { get; set; }
        public List<string> WayList { get; set; }
        public string OneTrip { get; set; }
        public string MultipleTrip { get; set; }
        public string MiddleTrip { get; set; }
    }
}