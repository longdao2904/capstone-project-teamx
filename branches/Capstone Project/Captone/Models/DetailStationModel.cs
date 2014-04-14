using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Captone.Models;
namespace Captone.Models
{
   
    public class DetailStationModel
    {
        public string RouteName { get; set; }
        public double duration { get; set; }
        public int RouteID { get; set; }
    }
}