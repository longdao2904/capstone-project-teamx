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
                                              group u by new {n.RequestID, k.Username, n.Request.DeliveryStatusID, k.ToLocation, k.SenderPhone, k.FromLocation, k.RequestCode, k.SenderName, k.ReceiverAddress, k.DateRequest }
                                                  into a

                                                  select new
                                            {
                                                a.Key.Username,
                                                a.Key.SenderName,
                                                a.Key.RequestID,
                                                a.Key.DeliveryStatusID,
                                                a.Key.ToLocation,
                                                a.Key.SenderPhone,
                                                a.Key.FromLocation,
                                                a.Key.RequestCode,
                                                a.Key.ReceiverAddress,
                                                a.Key.DateRequest
                                            }
                            )
                                         where p.DeliveryStatusID == 3 & p.FromLocation == stationID
                                         select new AssigningModel
                                            {
                                                Username = p.Username,
                                                RequestID = p.RequestID,
                                                ToLocation = p.ToLocation,
                                                SenderPhone = p.SenderPhone,
                                                RequestCode = p.RequestCode,
                                                SenderName =  p.SenderName,
                                                ReceiverAddress = p.ReceiverAddress,
                                                DateRequest = p.DateRequest
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
                           join c in _db.Coaches on l.Schedule.CoachID equals c.CoachID
                           join K in _db.Routes on l.Schedule.RouteID equals K.RouteID
                           join j in _db.Requests on p.RequestID equals j.RequestID
                           join m in _db.Stations on p.StopStation equals m.StationID
                           where p.TripID == item.TripID & p.RequestID == requestId
                           select new ListTripModel()
                           {
                               RouteName = K.RouteName,
                               RouteID = K.RouteID,
                               NumberPlate = c.NumberPlate,
                               EstimateArrivalTime = l.EstimateArrivalTime,
                               EstimateDepartureTime = l.EstimateDepartureTime,
                               TripID = p.TripID,
                               Status = l.Status,
                               StopStation = p.StopStation,
                               RequestID = p.RequestID,
                               IndicateOrder = p.IndicateOrder,
                               ToLocation = p.Request.ToLocation,
                               StopStationName = p.Station.StationName
                           }
                          ).FirstOrDefault();
                var listStage = FindStageFromRoute(get.RouteID);
                double tmpTime = 0;
                foreach (var stage in listStage)
                {
                    tmpTime += stage.Duration;
                    if (stage.EndPoint == get.StopStation) break;
                }
                get.TimeToMiddleStation = get.EstimateDepartureTime.AddHours(tmpTime);
                ass.Add(get);
            }
            ass.Sort((ass1, ass2) => ass1.IndicateOrder.CompareTo(ass2.IndicateOrder));
            return PartialView("ListTrip", ass);
        }
        public List<Stage> FindStageFromRoute(int routeID)
        {
            var routeStage = _db.RouteStages.ToList();
            var stages = new List<Stage>();
            var tmp = new List<RouteStage>();
            foreach (var stage in routeStage)
            {
                if (stage.RouteID == routeID) tmp.Add(stage);
            }
            tmp.Sort((stage1, stage2) => (stage1.StageIndex).CompareTo(stage2.StageIndex));
            foreach (var stage in tmp)
            {
                stages.Add(stage.Stage);
            }
            return stages;
        } 
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
                trip.Trip.RealDepartureTime = DateTime.Now;
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
                    if (trip.EstimateArrivalTime.TimeOfDay < DateTime.Now.TimeOfDay)
                    {
                        return Json(new { check = false }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new { check = true }, JsonRequestBehavior.AllowGet);
        }
        //
        public JsonResult CheckTripLate(List<Trip> tripId)
        {

            foreach (var trip in tripId)
            {
                if (trip.Status.Trim().Equals("Đang chạy"))
                {
                    if (trip.EstimateArrivalTime.TimeOfDay < DateTime.Now.TimeOfDay)
                    {
                        return Json(new { check = false, id = trip.TripID }, JsonRequestBehavior.AllowGet);
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
                                              group u by new { n.RequestID, k.Username, n.Request.DeliveryStatusID, n.StopStation, k.ToLocation, k.SenderPhone, k.RequestCode, k.SenderName }
                                                  into a

                                                  select new
                                                  {
                                                      a.Key.Username,
                                                      a.Key.RequestID,
                                                      a.Key.DeliveryStatusID,
                                                      a.Key.ToLocation,
                                                     
                                                      a.Key.SenderPhone,
                                                      a.Key.StopStation,
                                                      a.Key.RequestCode,
                                                      a.Key.SenderName
                                                  }
                            )
                                         where p.DeliveryStatusID == 4 & p.StopStation == stationId
                                         select new AssigningModel
                                         {
                                             Username = p.Username,
                                             RequestID = p.RequestID,
                                             ToLocation = p.ToLocation,
                                             SenderPhone = p.SenderPhone,
                                             StopStation = p.StopStation,
                                             RequestCode = p.RequestCode,
                                             SenderName = p.SenderName
                                         }
                       ).ToList();
            ViewBag.Assign = item;
            return View();
        }
        public ActionResult ListRequestPending(int stationId)
        {
            var getRequest = _db.Requests.Where(p => p.FromLocation == stationId & p.DeliveryStatusID == 4).ToList();
            List<Assigning> hehe = new List<Assigning>();
            foreach (var request in getRequest)
            {
                var getTrip = _db.Assignings.Where(p => p.RequestID == request.RequestID).FirstOrDefault();
                hehe.Add(getTrip);
            }

            return View(hehe);
        }
        public ActionResult ListTripPending(int requestId)
        {
            var list = _db.Assignings.Where(p => p.RequestID == requestId).ToList();
            List<ListTripModel> ass = new List<ListTripModel>();
            foreach (var item in list)
            {
                var get = (from p
                               in _db.Assignings
                           join l in _db.Trips on p.TripID equals l.TripID
                           join K in _db.Routes on l.Schedule.RouteID equals K.RouteID
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
                               RequestCode = j.RequestCode,
                               SenderName = j.SenderName
                           
                           }
                          ).FirstOrDefault();
                ass.Add(get);
            }
            ass.Sort((ass1, ass2) => (ass1.IndicateOrder).CompareTo(ass2.IndicateOrder));
            return PartialView("ListTripPeding", ass);
        }
        public ActionResult UpdateStatus(int tripId, int requestId)
        {
            var id = Session["StationID"];
            var list = _db.Assignings.Where(p => p.TripID == tripId & p.RequestID == requestId).Single();
            list.Trip.Status = "Đã đến";
            list.Trip.RealArrivalTime = DateTime.Now;
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
                assigning.Trip.RealDepartureTime = DateTime.Now;
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
                var list = _db.Requests.Where(r => r.DeliveryStatusID == 6 && r.Station.StationID == stationID).ToList();
                return View(list);
            }
            else
            {
                return View();
            }
        }
        //list request delivered to the customer at the current station
        public ActionResult FinishedRequest(int stationID)
        {
            if (stationID != 0)
            {
                var list = _db.Requests.Where(r => r.DeliveryStatusID == 6 && r.Station1.StationID == stationID).ToList();
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
                var list = _db.Requests.Where(r => r.DeliveryStatusID == 8 && r.ToLocation == stationID).ToList();
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
                var list = _db.Requests.Where(r => r.DeliveryStatusID == 5 && r.ToLocation == stationID).ToList();
                return View(list);
            }
            return View();
        }
    }
}
