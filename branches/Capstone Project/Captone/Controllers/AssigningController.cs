using Captone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services;
namespace Captone.Controllers
{
    public class AssigningController : Controller
    {
        iDeliverEntities _db = new iDeliverEntities();
        //
        public ActionResult ListAssign(int stationID)
        {
            List<AssigningModel> item = (from p
                                             in
                                             (from n in _db.Assignings
                                              join k in _db.Requests on n.RequestID equals k.RequestID
                                              join u in _db.Trips on n.TripID equals u.TripID
                                              group u by new {n.RequestID, k.Username, n.Request.DeliveryStatusID, k.ToLocation, k.SenderPhone, k.FromLocation, k.RequestCode }
                                                  into a

                                                  select new
                                            {
                                                a.Key.Username,
                                                a.Key.RequestID,
                                                a.Key.DeliveryStatusID,
                                                a.Key.ToLocation,
                                                a.Key.SenderPhone,
                                                a.Key.FromLocation,
                                                a.Key.RequestCode
                                            }
                            )
                                         where p.DeliveryStatusID == 3 & p.FromLocation == stationID
                                         select new AssigningModel
                                            {
                                                Username = p.Username,
                                                RequestID = p.RequestID,
                                                ToLocation = p.ToLocation,
                                                SenderPhone = p.SenderPhone,
                                                RequestCode = p.RequestCode
                                            }
                       ).ToList();
            ViewBag.Assign = item;
            return View("ListAssign");
        }
        //
        public ActionResult ListTrip(int requestId)
        {
            var list = _db.Assignings.Where(p => p.RequestID == requestId).ToList();
            List<ListTripModel> ass = new List<ListTripModel>();
            foreach (var item in list)
            {
                var get = (from p
                               in _db.Assignings
                           join l in _db.Trips on p.TripID equals l.TripID
                           join c in _db.Coaches on l.CoachID equals c.CoachID
                           join K in _db.Routes on l.RouteID equals K.RouteID
                           join j in _db.Requests on p.RequestID equals j.RequestID
                           join m in _db.Stations on p.StopStation equals m.StationID
                           where p.TripID == item.TripID & p.RequestID == requestId
                           select new ListTripModel()
                           {
                               RouteName = K.RouteName,
                               NumberPlate = c.NumberPlate,
                               EstimateArrivalTime = l.EstimateArrivalTime,
                               EstimateDepartureTime = l.EstimateDepartureTime,
                               TripID = p.TripID,
                               Status = l.Status,
                               StopStation = p.StopStation,
                               RequestID = p.RequestID,
                               IndicateOrder = p.IndicateOrder,
                               ToLocation = p.Request.ToLocation,
                               StopStationName = p.Station.StationName,
                               Date = l.Date

                           }
                          ).FirstOrDefault();
                ass.Add(get);
            }
            return PartialView("ListTrip", ass);
        }
        ///
        public ActionResult GetFromToLocation(int requestId)
        {
            var list = _db.Requests.Where(p => p.RequestID == requestId).ToList();

            return PartialView("GetFromToLocation", list);
        }
        //
        [HttpPost]
        [WebMethod]
        public void Accept(List<Request> requestId)
        {
            foreach (var item in requestId)
            {
                var deliverStatus = _db.Requests.Where(p => p.RequestID == item.RequestID).Single();
                deliverStatus.DeliveryStatusID = 4;
                var trip = _db.Assignings.Where(p => p.RequestID == item.RequestID & p.IndicateOrder == 1).Single();
                trip.Trip.Status = "Đang chạy";
                trip.Trip.RealDepartureTime = DateTime.Now.TimeOfDay;
                _db.SaveChanges();
            }
        }
        //
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
        //
        public ActionResult RequestPeding(int stationId)
        {
            List<AssigningModel> item = (from p
                                             in
                                             (from n in _db.Assignings
                                              join k in _db.Requests on n.RequestID equals k.RequestID
                                              join u in _db.Trips on n.TripID equals u.TripID
                                              group u by new { u.Date, n.RequestID, k.Username, n.Request.DeliveryStatusID, n.StopStation, k.ToLocation, k.SenderPhone, k.RequestCode }
                                                  into a

                                                  select new
                                                  {
                                                      a.Key.Username,
                                                      a.Key.RequestID,
                                                      a.Key.Date,
                                                      a.Key.DeliveryStatusID,
                                                      a.Key.ToLocation,
                                                      a.Key.SenderPhone,
                                                      a.Key.StopStation,
                                                      a.Key.RequestCode
                                                  }
                            )
                                         where p.DeliveryStatusID == 4 & p.StopStation == stationId
                                         select new AssigningModel
                                         {
                                             Username = p.Username,
                                             RequestID = p.RequestID,
                                             Date = p.Date,
                                             ToLocation = p.ToLocation,
                                             SenderPhone = p.SenderPhone,
                                             StopStation = p.StopStation,
                                             RequestCode = p.RequestCode
                                         }
                       ).ToList();
            ViewBag.Assign = item;
            return View();
        }
        //
        public ActionResult ListTripPeding(int requestId)
        {
            var list = _db.Assignings.Where(p => p.RequestID == requestId).ToList();
            List<ListTripModel> ass = new List<ListTripModel>();
            foreach (var item in list)
            {
                var get = (from p
                               in _db.Assignings
                           join l in _db.Trips on p.TripID equals l.TripID
                           join K in _db.Routes on l.RouteID equals K.RouteID
                           join j in _db.Requests on p.RequestID equals j.RequestID
                           join m in _db.Stations on p.StopStation equals m.StationID
                           where p.TripID == item.TripID & p.RequestID == requestId
                           select new ListTripModel()
                           {
                               RouteName = K.RouteName,
                               EstimateArrivalTime = l.EstimateArrivalTime,
                               EstimateDepartureTime = l.EstimateDepartureTime,
                               TripID = p.TripID,
                               Status = l.Status,
                               StopStation = p.StopStation,
                               RequestID = p.RequestID,
                               IndicateOrder = p.IndicateOrder,
                               ToLocation = p.Request.ToLocation,
                               StopStationName = p.Station.StationName,
                               Date = l.Date

                           }
                          ).FirstOrDefault();
                ass.Add(get);
            }
            return PartialView("ListTripPeding", ass);
        }
        public ActionResult UpdateStatus(int tripId, int requestId)
        {
            var id = Session["StationID"];
            var list = _db.Assignings.Where(p => p.TripID == tripId & p.RequestID == requestId).Single();
            list.Trip.Status = "Đã đến";
            list.Trip.RealArrivalTime = DateTime.Now.TimeOfDay;
            _db.SaveChanges();
            return RedirectToAction("RequestPeding", new { stationId = id });
        }
        public ActionResult NextStage(int tripId, int IndicaterOrder)
        {
            var id = Session["StationID"];
            var list2 = _db.Assignings.Where(p => p.TripID == tripId & p.IndicateOrder == IndicaterOrder).ToList();
            foreach (var assigning in list2)
            {

                assigning.Trip.Status = "Đang chạy";
                assigning.Trip.RealDepartureTime = DateTime.Now.TimeOfDay;
                _db.SaveChanges();


            }
            return RedirectToAction("RequestPeding", new { stationId = id });
        }
        //
        public ActionResult ListRequestForTrip(int tripid)
        {
            List<Request> requests = new List<Request>();
            var list = _db.Assignings.Where(p => p.TripID == tripid).ToList();
            foreach (var item in list)
            {
                var request = _db.Requests.Where(p => p.RequestID == item.RequestID & p.DeliveryStatusID == 3).FirstOrDefault();
                if (request != null)
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
        //
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
        public ActionResult UpdateArrive(int requestId)
        {
            var list = _db.Requests.Where(p => p.RequestID == requestId).Single();
            list.DeliveryStatusID = 5;
            _db.SaveChanges();
            var id = Session["StationID"];
            return RedirectToAction("RequestPeding", new { stationId = id });
        }
        // List requests/packages delivered
        public ActionResult DeliveredRequest(int stationID)
        {
            if (stationID != 0)
            {
                var list = _db.Requests.Where(r => r.DeliveryStatusID == 6 && r.Station.StationID == stationID | r.Station1.StationID == stationID).ToList();
                return View(list);
            }
            else
            {
                return View();
            }
        }
        // List requests/packages expired/cannot delivered
        public ActionResult ExpiredRequest(int stationID)
        {
            if (stationID != 0)
            {
                var list = _db.Requests.Where(r => r.DeliveryStatusID == 8 && r.Station.StationID == stationID | r.Station1.StationID == stationID).ToList();
                return View(list);
            }
            else
            {
                return View();
            }
        }
        // List requests/packages arrived at endpoint station
        public ActionResult ArrivedRequest(int stationID)
        {
            if (stationID != 0)
            {
                var list = _db.Requests.Where(r => r.DeliveryStatusID == 5 && r.Station.StationID == stationID | r.Station1.StationID == stationID).ToList();
                return View(list);
            }
            else
            {
                return View();
            }

        }
    }
}
