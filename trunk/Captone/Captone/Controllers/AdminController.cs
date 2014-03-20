using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Captone.Models;

namespace Captone.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        iDeliverEntities _db = new iDeliverEntities();
     public ActionResult PieChart()
     {
         var RouteID = _db.Routes.ToList();
         ArrayList array = new ArrayList();
         foreach (var item in RouteID)
         {
             var countRoute = _db.Trips.Where(p => p.RouteID == item.RouteID).Count();
             var routeName = _db.Routes.Where(p => p.RouteID == item.RouteID).Single();
             array.Add(countRoute + " : " + routeName.RouteName);
         }
         return Json(array, JsonRequestBehavior.AllowGet);
     }

    }
}
