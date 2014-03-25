using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Captone.Models;
namespace Captone.Controllers
{
    public class AssigningController : Controller
    {
        //
        // GET: /Assigning/
        iDeliverEntities _db = new iDeliverEntities();
        public ActionResult ListAssign()
        {
            List<AssigningModel> item = (from p
                                             in
                                             (from n in _db.Assignings   
                                                join k in _db.Requests on n.RequestID equals k.RequestID
                                                join u in _db.Trips on n.TripID equals u.TripID
                                                group u by new {u.Date, n.RequestID, k.Username, n.Request.DeliveryStatusID, k.ToLocation,k.SenderPhone}
                                                into a
                                                 
                                              select new
                                        {
                                            a.Key.Username,
                                            a.Key.RequestID,
                                            a.Key.Date,
                                            a.Key.DeliveryStatusID,
                                            a.Key.ToLocation,
                                            a.Key.SenderPhone
                                        }
                            )
                            where p.DeliveryStatusID == 3
                                select new AssigningModel
                                   {
                                     Username  = p.Username,
                                     RequestID  = p.RequestID,
                                     Date  = p.Date,
                                     ToLocation = p.ToLocation,
                                     SenderPhone = p.SenderPhone
                                   }
                       ).ToList();
            ViewBag.Assign = item;
            return PartialView("ListAssign");
        }
        public ActionResult ListTrip(int requestId)
        {
            var list = _db.Assignings.Where(p => p.RequestID == requestId).ToList();
            return PartialView("ListTrip", list);
        }
        public ActionResult GetFromToLocation(int requestId)
        {
            var list = _db.Requests.Where(p => p.RequestID == requestId).ToList();
            return PartialView("GetFromToLocation", list);
        }
        [HttpPost]
        [WebMethod]
        public void Accept(List<Request> requestId )
        {
            foreach (var item in requestId)
            {
                var deliverStatus = _db.Requests.Where(p => p.RequestID == item.RequestID).Single();
                deliverStatus.DeliveryStatusID = 4;
                
                _db.SaveChanges();
            }
        }
        public ActionResult getRequestAssign(int StationID)
        {
            var list =
                _db.Requests.Where(
                    p => (p.Station.StationID == StationID | p.Station1.StationID == StationID) & p.DeliveryStatusID == 2).ToList();
            ;
            return View(list);
        }
    }
}
