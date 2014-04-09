using System.Data.Objects.SqlClient;
using Captone.Models;
using Captone.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services;

namespace Captone.Controllers
{
    public class TripController : Controller
    {
        private iDeliverEntities db = new iDeliverEntities();

        #region CRUD
        public ActionResult Index()
        {
            //if (Session["USERNAME"] == null)
            //{
            //    return RedirectToAction("LogIn", "Account");
            //}
            //else
            //{
            //    var trips = db.Trips;
            //    return View(trips.ToList());
            //}
            var trips = db.Trips;
            return View(trips.ToList());
        }

        //
        // POST: /Trip/ListTrip
        [HttpPost]
        public ActionResult ListTrip(DateTime tripDate)
        {
            var trips = db.Trips.Where(t => t.EstimateDepartureTime == tripDate);
            return View(trips.ToList());
        }

        //
        // GET: /Trip/Details/5

        public ActionResult Details(int id = 0)
        {
            //if (Session["USERNAME"] == null)
            //{
            //    return RedirectToAction("LogIn", "Account");
            //}
            //else
            //{
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
            //}
        }

        //
        // GET: /Trip/Create

        public ActionResult Create(int stationID)
        {
            //string username = (string)Session["USERNAME"];
            //string role = (string)Session["UserRole"];
            //if (username != null && role == "Staff")
            //{

            ViewData["RouteID"] = (from p
                                       in db.RouteStages
                                   join l in db.Stages on p.StageID equals l.StageID
                                   where p.StageIndex == 1 & p.Stage.StartPoint == stationID
                                   select new SelectListItem()
                                              {
                                                  Text = p.Route.RouteName,
                                                  Value = SqlFunctions.StringConvert((double)p.RouteID)
                                              }
                                  ).ToList();
            return View();
            //}
            //else
            //{
            //    return RedirectToAction("LogIn", "Account");
            //}
        }

        //
        // POST: /Trip/Create

        [HttpPost]
        [WebMethod]
        public ActionResult Create(List<Trip> trips, DateTime date)
        {
            if (ModelState.IsValid)
            {
                if (trips != null)
                {
                    foreach (var trip in trips)
                    {
                        var schedule = db.Schedules.FirstOrDefault(s => s.ScheduleID == trip.ScheduleID);
                        var route = schedule.CoachArrangement.Route;
                        var stages = db.RouteStages.Where(r => r.RouteID == route.RouteID).ToList();
                        var deltaTime = 0.0;
                        foreach (var stage in stages)
                        {
                            deltaTime += (stage.Stage.Duration);
                        }
                        Trip t = new Trip();
                        var tmpTime = date.Add(trip.EstimateDepartureTime.TimeOfDay);
                        t.RouteID = trip.RouteID;
                        t.CoachID = trip.CoachID;
                        t.ScheduleID = trip.ScheduleID;
                        // Estimate volume = Capacity of coach run this trip * max volume level of route where trip run

                        double capacity = schedule.CoachArrangement.Coach.CoachType.Capacity;
                        double container = schedule.CoachArrangement.Route.Container.Value;
                        t.EstimateVolume = capacity * container;
                        t.AvailableVolume = t.EstimateVolume;
                        t.IsActive = true;
                        t.Status = "Chưa chạy";
                        for (int i = 0; i < 7; i++)
                        {
                            t.EstimateDepartureTime = tmpTime.AddDays(i);
                            t.EstimateArrivalTime = tmpTime.AddDays(i).AddHours(deltaTime);
                            db.Trips.Add(t);
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Trip/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //if (Session["USERNAME"] == null)
            //{
            //    return RedirectToAction("LogIn", "Account");
            //}
            //else
            //{
            Trip trip = db.Trips.Find(id);
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName");
            ViewBag.CoachID = new SelectList(db.Coaches, "CoachID", "NumberPlate");
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
            //}
        }

        //
        // POST: /Trip/Edit/5

        [HttpPost]
        public ActionResult Edit(Trip trip)
        {
            //if (Session["USERNAME"] == null)
            //{
            //    return RedirectToAction("LogIn", "Account");
            //}
            //else
            //{
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName");
            ViewBag.CoachID = new SelectList(db.Coaches, "CoachID", "NumberPlate");
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trip);
            //}
        }

        //
        // GET: /Trip/Delete/5

        public ActionResult Delete(int id = 0)
        {
            //if (Session["USERNAME"] == null)
            //{
            //    return RedirectToAction("LogIn", "Account");
            //}
            //else
            //{
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
            //}
        }

        //
        // POST: /Trip/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Trip trip = db.Trips.Find(id);
            db.Trips.Remove(trip);
            Assigning asg = db.Assignings.Where(a => a.TripID == id).FirstOrDefault();
            db.Assignings.Remove(asg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult ListTripForRequest()
        {
            var listTrip = db.Trips.ToList();
            return PartialView("ListTripForRequest", listTrip);
        }

        [HttpPost]
        [WebMethod]
        public void Assigning(List<Request> request)
        {
            AssigningService assign = new AssigningService();

            var result = assign.Assigning(request);
            foreach (var item in result)
            {
                for (var i = 0; i < item.Value.Count; i++)
                {
                    Assigning ass = new Assigning();
                    var requestId = item.Key.RequestID;
                    ass.RequestID = requestId;
                    var tripId = item.Value.Keys.ToList();
                    var stop = item.Value.Values.ToList();
                    ass.TripID = tripId[i].TripID;
                    ass.StopStation = stop[i];
                    ass.IndicateOrder = i + 1;
                    ass.AssignedDate = DateTime.Now;
                    var req = db.Requests.Where(p => p.RequestID == requestId).Single();
                    req.DeliveryStatusID = 3;
                    db.Assignings.Add(ass);
                    db.SaveChanges();
                }
            }
        }

        //Pass estimated time to real time of trip when depart/arrive
        [HttpPost]
        [WebMethod]
        public ActionResult TimePassing(List<Trip> trips)
        {
            if (trips != null)
            {
                foreach (var trip in trips)
                {
                    DateTime current = DateTime.Now;
                    if (trip.EstimateDepartureTime.ToShortDateString() == current.ToShortDateString())
                    {
                        Trip t = db.Trips.FirstOrDefault(tr => tr.TripID == trip.TripID);
                        t.RealDepartureTime = trip.EstimateDepartureTime;
                        t.RealArrivalTime = trip.EstimateArrivalTime;
                        db.Entry(t).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Request");
                    }
                }
            }
            return View();
        }

        //For view assigned request of selected trip
        [HttpPost]
        [WebMethod]
        public ActionResult AssignedRequest(int tripID)
        {
            List<Request> listRequests = new List<Request>();
            List<int> rqID = new List<int>();
            rqID = (from a in db.Assignings where a.TripID == tripID select a.RequestID).ToList();
            foreach (var id in rqID)
            {
                Request rq = db.Requests.Where(r => r.RequestID == id).FirstOrDefault();
                listRequests.Add(rq);
            }
            return View(listRequests);
        }

        #region load and re-assigned requests of selected trip to be deleted
        // Retrieve requests assigned to selected trip, find by TripID
        [HttpPost]
        [WebMethod]
        public ActionResult RelatedRequests(int tripID)
        {
            List<Request> listRequests = new List<Request>();
            List<int> rqID = new List<int>();
            rqID = (from a in db.Assignings where a.TripID == tripID select a.RequestID).ToList();
            foreach (var id in rqID)
            {
                Request rq = db.Requests.Where(r => r.RequestID == id).FirstOrDefault();
                listRequests.Add(rq);
            }
            return View(listRequests);
        }
        // Set status of related request to 'Đã xác nhận - 2' to re-assigned
        [HttpPost]
        [WebMethod]
        public void UpdateRequests(int tripID)
        {
            List<Trip> _trips = new List<Trip>();
            List<Assigning> _assignings = db.Assignings.ToList();
            List<Request> listRequest = new List<Models.Request>();

            // list of requestIDs to be updated/re-assigned
            var tmpRequestID = new List<int>();

            foreach (var assigning in _assignings)
            {
                if (assigning.TripID == tripID)
                {
                    // get list of request id that related to delete trip
                    tmpRequestID.Add(assigning.RequestID);
                }
            }

            //update the status of all request in the list

            foreach (var i in tmpRequestID)
            {
                Request request = db.Requests.Where(r => r.RequestID == i).FirstOrDefault();
                listRequest.Add(request);
                foreach (var rq in listRequest)
                {
                    rq.DeliveryStatusID = 2;
                    db.Entry(rq).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
        #endregion

        // Load trips arrived destination
        [WebMethod]
        public ActionResult ArrivedTrip()
        {
            List<Trip> trips = new List<Trip>();
            int stationID = int.Parse(Session["StationID"].ToString());
            trips = (from t in db.Trips
                     where t.TripID ==
                         ((from a in db.Assignings
                           where a.RequestID ==
                               ((from r in db.Requests
                                 where r.DeliveryStatusID == 5 && r.ToLocation == stationID
                                 select new { r.RequestID }).FirstOrDefault().RequestID)
                           select new { a.TripID }).FirstOrDefault().TripID)
                     select t).ToList();
            return View(trips);
        }

        // Load trip depart from start station
        [WebMethod]
        public ActionResult DepartedTrip()
        {
            List<Trip> trips = new List<Trip>();
            int stationID = int.Parse(Session["StationID"].ToString());
            trips = (from t in db.Trips
                     where t.TripID ==
                         ((from a in db.Assignings
                           where a.RequestID ==
                               ((from r in db.Requests
                                 where r.DeliveryStatusID == 4 && r.FromLocation == stationID
                                 select new { r.RequestID }).FirstOrDefault().RequestID)
                           select new { a.TripID }).FirstOrDefault().TripID)
                     select t).ToList();
            List<Trip> list = new List<Trip>();
            foreach (var item in trips)
            {
                TimeSpan latetime = new TimeSpan();
                TimeSpan estimateArrival = new TimeSpan();
                latetime = (TimeSpan)(item.RealDepartureTime - item.EstimateDepartureTime);
                estimateArrival = (item.EstimateArrivalTime.TimeOfDay + latetime);
                item.EstimateArrivalTime.Add(estimateArrival);
                list.Add(item);
            }
            return View(list);
        }

        // Update assigned requests of departed trip after click button 'Xe đã chạy'
        [HttpPost]
        [WebMethod]
        public void DepartedRequest(int tripID, int stationID)
        {
            List<Assigning> _assignings = db.Assignings.ToList();
            List<Request> listRequest = new List<Request>();
            var trip = db.Trips.Where(t => t.TripID == tripID).FirstOrDefault();
            // list of requestIDs which departed
            var tmpRequestIDs = new List<int>();
            var asg = _assignings.Where(a => a.TripID == tripID).FirstOrDefault();
            if (trip.Route.RouteStages.First().Stage.StartPoint == stationID)
            {
                // get list of request id that related to departed trip
                tmpRequestIDs.Add(asg.RequestID);
            }
            // update real departed time and status of trip
            trip.RealDepartureTime = DateTime.Now;
            trip.Status = "Đang chạy";
            db.Entry(trip).State = EntityState.Modified;
            db.SaveChanges();
            //update the status of all request in the list
            foreach (var i in tmpRequestIDs)
            {
                Request request = db.Requests.Where(r => r.RequestID == i).FirstOrDefault();
                listRequest.Add(request);
                foreach (var rq in listRequest)
                {
                    rq.DeliveryStatusID = 4;
                    db.Entry(rq).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        // Update assigned requests of arrived trip after click button 'Xe đã đến trạm'
        [HttpPost]
        [WebMethod]
        public void ArrivedRequest(int tripID, int stationID)
        {
            List<Assigning> _assignings = db.Assignings.ToList();
            List<Request> listRequest = new List<Request>();
            var trip = db.Trips.Where(t => t.TripID == tripID).FirstOrDefault();
            // list of requestIDs which arrived
            var tmpRequestIDs = new List<int>();
            var asg = _assignings.Where(a => a.TripID == tripID).FirstOrDefault();
            var listRq = new List<int>();
            listRq = (from a in db.Assignings where a.TripID == tripID select a.RequestID).ToList();
            var listREQUEST = new List<Request>();
            foreach (var id in listRq)
            {
                Request req = db.Requests.Where(r => r.RequestID == id).FirstOrDefault();
                listREQUEST.Add(req);
            }
            // endpoint of stage = stationID : request arrived endstation
            foreach (var item in listREQUEST)
            {
                if (asg.StopStation == stationID && item.ToLocation == stationID)
                {
                    // get list of request id that related to arrived trip
                    tmpRequestIDs.Add(asg.RequestID);
                    //update the status of all request in the list
                    foreach (var i in tmpRequestIDs)
                    {
                        Request request = db.Requests.Where(r => r.RequestID == i).FirstOrDefault();
                        listRequest.Add(request);
                        foreach (var rq in listRequest)
                        {
                            rq.ArrivedDate = DateTime.Now.Date;
                            rq.DeliveryStatusID = 5;
                            db.Entry(rq).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    // update real arrival time of trip
                    trip.RealArrivalTime = DateTime.Now;
                    trip.Status = "Đã đến đích";
                    db.Entry(trip).State = EntityState.Modified;
                    db.SaveChanges();
                }
                // stop station from assigning = stationID : request arrived stop station
                else if (asg.StopStation == stationID)
                {
                    // update real arrival time of trip
                    trip.RealArrivalTime = DateTime.Now;
                    trip.Status = "Đã đến đích";
                    db.Entry(trip).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
    }
}