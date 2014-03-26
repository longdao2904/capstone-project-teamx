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
        public ActionResult ListAssign(int stationID)
        {
            List<AssigningModel> item = (from p
                                             in
                                             (from n in _db.Assignings
                                              join k in _db.Requests on n.RequestID equals k.RequestID
                                              join u in _db.Trips on n.TripID equals u.TripID
                                              group u by new { u.Date, n.RequestID, k.Username, n.Request.DeliveryStatusID, k.ToLocation, k.SenderPhone, k.FromLocation }
                                                  into a

                                                  select new
                                            {
                                                a.Key.Username,
                                                a.Key.RequestID,
                                                a.Key.Date,
                                                a.Key.DeliveryStatusID,
                                                a.Key.ToLocation,
                                                a.Key.SenderPhone,
                                                a.Key.FromLocation
                                            }
                            )
                                         where p.DeliveryStatusID == 3 & p.FromLocation == stationID
                                         select new AssigningModel
                                            {
                                                Username = p.Username,
                                                RequestID = p.RequestID,
                                                Date = p.Date,
                                                ToLocation = p.ToLocation,
                                                SenderPhone = p.SenderPhone
                                            }
                       ).ToList();
            ViewBag.Assign = item;
            return View("ListAssign");
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
        public void Accept(List<Request> requestId)
        {
            foreach (var item in requestId)
            {
                var deliverStatus = _db.Requests.Where(p => p.RequestID == item.RequestID).Single();
                deliverStatus.DeliveryStatusID = 4;
                var trip = _db.Assignings.Where(p => p.RequestID == item.RequestID & p.IndicateOrder == 1).Single();
                trip.Trip.RealDepartureTime = DateTime.Now.TimeOfDay;
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

        public JsonResult CheckRequestLate(List<Trip> requestId)
        {

            foreach (var trip in requestId)
            {
                if (trip.Status.Trim().Equals("Đang chạy"))
                {
                    if (trip.EstimateArrivalTime > DateTime.Now.TimeOfDay)
                    {
                        return Json(new { check = false }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new { check = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RequestPeding()
        {
            List<AssigningModel> item = (from p
                                             in
                                             (from n in _db.Assignings
                                              join k in _db.Requests on n.RequestID equals k.RequestID
                                              join u in _db.Trips on n.TripID equals u.TripID
                                              group u by new { u.Date, n.RequestID, k.Username, n.Request.DeliveryStatusID, k.ToLocation, k.SenderPhone }
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
                                         where p.DeliveryStatusID == 4
                                         select new AssigningModel
                                         {
                                             Username = p.Username,
                                             RequestID = p.RequestID,
                                             Date = p.Date,
                                             ToLocation = p.ToLocation,
                                             SenderPhone = p.SenderPhone
                                         }
                       ).ToList();
            ViewBag.Assign = item;
            return View();
        }
        public ActionResult ListTripPeding(int requestId)
        {
            var list = _db.Assignings.Where(p => p.RequestID == requestId).ToList();
            return PartialView("ListTripPeding", list);
        }

        public ActionResult ListRequestForTrip(int tripid)
        {
            List<Request> requests = new List<Request>();
            var list = _db.Assignings.Where(p => p.TripID == tripid).ToList();
            foreach (var item in list)
            {
                var request = _db.Requests.Where(p => p.RequestID == item.RequestID & p.DeliveryStatusID == 3).FirstOrDefault();
                if(request!=null)
                {
                    requests.Add(request);
                }
            }
            if (requests.Count > 0)
            {
                return View(requests);
            }
            return View(requests);
        }
        [WebMethod]
        public void UpdateRequestLate(List<Request> requestId)
        {
            foreach (var i in requestId)
            {
                var assigning = _db.Assignings.Where(p => p.RequestID == i.RequestID).Single();
                var list = _db.Requests.Where(p => p.RequestID == i.RequestID).Single();
                list.DeliveryStatusID = 2;
                _db.Assignings.Remove(assigning);
                _db.SaveChanges();
            }

        }
    }
}
