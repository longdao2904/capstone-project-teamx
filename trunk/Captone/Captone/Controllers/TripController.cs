using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services;
using Captone.Models;
using Captone.Services;

namespace Captone.Controllers
{
    public class TripController : Controller
    {
        private iDeliverEntities db = new iDeliverEntities();

        #region CRUD
        //
        // GET: /Trip/

        public ActionResult Index()
        {
            var trips = db.Trips;
            return View(trips.ToList());
        }

        //
        // POST: /Trip/ListTrip
        [HttpPost]
        public ActionResult ListTrip(DateTime tripDate)
        {
            var trips = db.Trips.Where(t => t.Date == tripDate);
            return View(trips.ToList());
        }

        //
        // GET: /Trip/Details/5

        public ActionResult Details(int id = 0)
        {
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        //
        // GET: /Trip/Create

        public ActionResult Create()
        {
            string username = (string)Session["USERNAME"];
            string role = (string)Session["UserRole"];
            if (username != null && role == "Staff")
            {
                ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName");
                return View();
            }
            else
            {
                return RedirectToAction("LogOn", "Account");
            }
        }

        //
        // POST: /Trip/Create

        [HttpPost]
        [WebMethod]
        public ActionResult Create(List<Trip> trips)
        {
            if (ModelState.IsValid)
            {
                if (trips != null)
                {
                    foreach (var trip in trips)
                    {
                        Trip t = new Trip();
                        t.EstimateDepartureTime = trip.EstimateDepartureTime;
                        t.EstimateArrivalTime = trip.EstimateArrivalTime;
                        t.RouteID = trip.RouteID;
                        t.CoachID = trip.CoachID;
                        t.ScheduleID = trip.ScheduleID;
                        t.EstimateVolume = trip.EstimateVolume;
                        for (int i = 0; i < 7; i++)
                        {
                            TimeSpan day = new TimeSpan();
                            if (i == 0)
                            {
                                day = new TimeSpan(0, 0, 0, 0);
                            }
                            else
                            {
                                day = new TimeSpan(i / i, 0, 0, 0);
                            }
                            DateTime nextday = trip.Date.Add(day);
                            trip.Date = nextday.Date;
                            t.Date = trip.Date.Date;
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
            Trip trip = db.Trips.Find(id);
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName");
            ViewBag.CoachID = new SelectList(db.Coaches, "CoachID", "NumberPlate");
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        //
        // POST: /Trip/Edit/5

        [HttpPost]
        public ActionResult Edit(Trip trip)
        {
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName");
            ViewBag.CoachID = new SelectList(db.Coaches, "CoachID", "NumberPlate");
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        //
        // GET: /Trip/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
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

            var result = assign.Assigning(request, DateTime.Now);
            foreach (var item in result)
            {
                for (var i = 0; i < item.Value.Count; i++)
                {
                    Assigning ass = new Assigning();
                    var requestId = item.Key.RequestID;
                    ass.RequestID = requestId;
                    var tripId = item.Value[i].TripID;
                    ass.TripID = tripId;
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
                    if (trip.Date.ToShortDateString() == current.ToShortDateString())
                    {
                        Trip t = db.Trips.Where(tr => tr.TripID == trip.TripID).FirstOrDefault();
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

        // Retrieve requests assigned to selected trip, find by TripID
        [HttpPost]
        [WebMethod]
        public ActionResult RelatedRequests(int tripID)
        {
            //List<Request> requests = new List<Request>();
            //requests = (from r in db.Requests
            //           where r.RequestID == (from a in db.Assignings
            //                                where a.TripID == tripID
            //                                select a.RequestID) select r).ToList();
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

        [HttpPost]
        [WebMethod]
        public void UpdateRequests(int tripID)
        {
            iDeliverEntities db = new iDeliverEntities();
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
    }
}