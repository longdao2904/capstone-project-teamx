using System.Data.Objects.SqlClient;
using System.Diagnostics;
using System.IO;
using Captone.Models;
using Captone.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services;
using Newtonsoft.Json;

namespace Captone.Controllers
{
    public class TripController : Controller
    {
        private iDeliverEntities db = new iDeliverEntities();
        double _deltaTime = 2.0;  //the time between two times the trip run
        private int _numberOfTrip = 100;
        //list all trip of the system
        public ActionResult Index()
        {
            var trips = db.Trips.ToList();
            return View(trips);
        }

        //list all new trip of the current station
        public ActionResult NewTrip(int stationID)
        {
            var trips = db.Trips.Where(i => i.Status == "Chưa chạy").ToList();
            trips = Filter(trips, stationID, true);
            return View(trips);
        }

        //list all trip passing this station
        public ActionResult OtherTrip(int stationID)
        {
            var trips = db.Trips.ToList();
            trips = Filter(trips, stationID, false);
            return View(trips);
        }

        //if type = true, mean list trip from the current station, othewise, list trip from the other station
        public List<Trip> Filter(List<Trip> trips, int stationID, bool type)
        {
            var listTrips = new List<Trip>();
            foreach (var trip in trips)
            {
                int routeID = trip.Schedule.Route.RouteID;
                var stages = FindStageFromRoute(routeID);
                if (type && stages.First().StartPoint == stationID) listTrips.Add(trip);
                if (!type)
                {
                    var flag = false;
                    foreach (var stage in stages)
                    {
                        if (stage.EndPoint == stationID && (trip.Status == "Đang chạy")) flag = true;
                    }
                    if (flag) listTrips.Add(trip);
                }
            }
            return listTrips;
        }
        public List<Trip> Filter2(List<Trip> trips, int stationID, bool type)
        {
            var listTrips = new List<Trip>();
            foreach (var trip in trips)
            {
                int routeID = trip.Schedule.Route.RouteID;
                var stages = FindStageFromRoute(routeID);
                if (type && stages.First().StartPoint == stationID) listTrips.Add(trip);
                if (!type)
                {
                    var flag = false;
                    foreach (var stage in stages)
                    {
                        if (stage.EndPoint == stationID && trip.Status == "Đã đến trạm"
                            && trip.RealArrivalTime == null) flag = true;
                    }
                    if (flag) listTrips.Add(trip);
                }
            }
            return listTrips;
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
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        //
        // GET: /Trip/Create
        [HttpGet]
        public ActionResult Create(int stationID)
        {
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
            return PartialView();
        }

        //
        // POST: /Trip/Create

        [HttpPost]
        [WebMethod]
        public ActionResult Create(List<Schedule> schedules, DateTime date)
        {
            if (ModelState.IsValid)
            {
                if (schedules != null)
                {
                    foreach (var schedule in schedules)
                    {
                        var t = new Trip();
                        //add parameter for new trip:
                        //schedule of trip
                        t.ScheduleID = schedule.ScheduleID;
                        //estimate volume of route
                        t.EstimateVolume = FindVolumeFromSchedule(schedule);
                        //at first, set available volume equal estimate volume
                        t.AvailableVolume = t.EstimateVolume;
                        t.RealVolume = 0;
                        t.IsActive = true;
                        t.Status = "Chưa chạy";
                        var tmpTime = GetNextTime(schedule) > date ? GetNextTime(schedule) : date;
                        tmpTime = tmpTime.Add(schedule.EstimateDepartureTime);
                        if (GetTime(schedule.RouteID) < (24 - _deltaTime) / 2)
                        {
                            for (int i = 0; i < _numberOfTrip; i++)
                            {
                                t.EstimateDepartureTime = tmpTime.AddDays(i);
                                t.EstimateArrivalTime = tmpTime.AddDays(i).AddHours(GetTime(schedule.RouteID));
                                db.Trips.Add(t);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _numberOfTrip; i++)
                            {
                                t.EstimateDepartureTime = tmpTime.AddDays(2 * i);
                                t.EstimateArrivalTime = tmpTime.AddDays(2 * i).AddHours(GetTime(schedule.RouteID));
                                db.Trips.Add(t);
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public double FindVolumeFromSchedule(Schedule schedule)
        {
            var coachType = db.CoachTypes.ToList();
            var coaches = db.Coaches.ToList();
            var routes = db.Routes.ToList();
            var volume = 0.0;
            var container = 0.0;
            foreach (var coach in coaches)
            {
                if (coach.CoachID == schedule.CoachID)
                {
                    foreach (var type in coachType)
                    {
                        if (type.CoachTypeID == coach.CoachTypeID)
                        {
                            volume = type.Height * type.Length * type.Width;
                        }
                    }
                }
            }
            foreach (var route in routes)
            {
                if (route.RouteID == schedule.RouteID) container = route.Container;
            }
            return volume * container;
        }
        //get the earliest day for create new trip comparing to the list trip from database
        public DateTime GetNextTime(Schedule schedule)
        {
            var listTrip = db.Trips.ToList();
            var resTrip = new List<Trip>();
            foreach (var trip in listTrip)
            {
                if (schedule.ScheduleID == trip.ScheduleID) resTrip.Add(trip);
            }
            if (resTrip.Count == 0) return (DateTime.Now).Date;
            resTrip.Sort((trip1, trip2) => (trip1.EstimateArrivalTime).CompareTo(trip2.EstimateArrivalTime));
            var res = resTrip.Last().EstimateDepartureTime;
            res = res.AddHours(GetTime(schedule.RouteID) * 2 + _deltaTime * 2);
            if (res.Hour > schedule.EstimateDepartureTime.Hours) return res.Date;
            return res.Date.AddDays(1);
        }

        //find the list stage of route
        public List<Stage> FindStageFromRoute(int routeID)
        {
            var routeStage = db.RouteStages.ToList();
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
        //get the time that trip travel on the route
        public double GetTime(int routeID)
        {
            var res = 0.0;
            var stages = FindStageFromRoute(routeID);
            foreach (var stage in stages)
            {
                res += stage.Duration;
            }
            return res;
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
            var listAssign = db.Assignings.Where(i => i.TripID == id);
            foreach (var assigning in listAssign)
            {
                db.Assignings.Remove(assigning);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            db.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        [WebMethod]
        public ActionResult Assigning(List<Request> request)
        {
            var requests = request;
            if (request != null)
            {
                requests = new List<Request>();
                var assign = new AssigningService();
                var result = assign.Assigning(request);
                foreach (var rq in request)
                {
                    int flag = 0;
                    foreach (var item in result)
                    {
                        if (item.Key.RequestID == rq.RequestID) flag++;
                    }
                    if (flag == 0) requests.Add(rq);

                }
                foreach (var item in result)
                {
                    for (var i = 0; i < item.Value.Count; i++)
                    {
                        var ass = new Assigning();
                        var requestId = item.Key.RequestID;
                        ass.RequestID = requestId;
                        var tripId = item.Value.Keys.ToList();
                        var stop = item.Value.Values.ToList();
                        ass.TripID = tripId[i].TripID;
                        ass.StopStation = stop[i];
                        ass.IndicateOrder = i + 1;
                        ass.AssignedDate = DateTime.Now;
                        var req = db.Requests.Single(p => p.RequestID == requestId);
                        req.DeliveryStatusID = 3;
                        db.Assignings.Add(ass);
                        db.SaveChanges();
                    }
                }
            }
            var stringView = RenderRazorViewToString("Assigning", requests);
            return Json(stringView, JsonRequestBehavior.AllowGet);
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        //For view assigned request of selected trip
        //Get assigned requests, not departed
        public ActionResult AssignedRequest(int tripID, int stationID)
        {
            var rqID = db.Assignings.Where(p => p.TripID == tripID).ToList();
            var listRequest = new List<Assigning>();
            foreach (var rq in rqID)
            {
                if (rq.Request.DeliveryStatusID == 4)
                {
                    var ass = db.Assignings.FirstOrDefault(p => (p.IndicateOrder == (rq.IndicateOrder - 1)) && p.RequestID == rq.RequestID);
                    if (ass.StopStation == stationID) listRequest.Add(rq);
                }
                if(rq.Request.DeliveryStatusID == 3) listRequest.Add(rq);
            }
            return View(listRequest);
        }
        //Get assigned requests & departed
        public ActionResult AssignedRequest2(int tripID, int stationID)
        {
            var rqID = db.Assignings.Where(p => p.TripID == tripID & p.Request.DeliveryStatusID == 4).ToList();
            var listRequest = new List<Assigning>();
            foreach (var rq in rqID)
            {
                if(rq.StopStation != stationID) listRequest.Add(rq);
            }
            return View(listRequest);
        }
        public bool CheckRequestLate(int tripID)
        {
            var all = db.Assignings.Where(p => p.TripID != tripID & p.Request.DeliveryStatusID == 4).ToList();
            var rqID = db.Assignings.Where(p => p.TripID == tripID & p.Request.DeliveryStatusID == 4).ToList();
            foreach (var assigning in rqID)
            {
                foreach (var assigning1 in all)
                {
                    if (assigning.RequestID == assigning1.RequestID)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #region load and re-assigned requests of selected trip to be deleted

        // Retrieve requests assigned to selected trip, find by TripID
        [HttpPost]
        [WebMethod]
        public ActionResult RelatedRequests(int tripID)
        {
            var listRequests = db.Assignings.Where(p => p.TripID == tripID & p.Request.DeliveryStatusID == 3).ToList();
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
        public ActionResult ArrivedTrip(int stationID)
        {
            var trips = new List<Trip>();
            var listTrip = db.Trips.ToList();
            foreach (var trip in listTrip)
            {
                if (FindStageFromRoute(trip.Schedule.RouteID).First().StartPoint == stationID
                    && trip.RealArrivalTime != null) trips.Add(trip);
            }
            return View(trips);
        }

        // Load trip depart from start station
        [WebMethod]
        public ActionResult DepartedTrip(int stationID)
        {
            var trips = db.Trips.ToList();
            trips = Filter(trips, stationID, true);
            var tmpTrip = trips.Where(trip => trip.Status == "Đang chạy").ToList();
            return View(tmpTrip);
        }

        //find all request assigned to a given trip
        public List<Request> FindRequestAssignToTrip(int tripID)
        {
            //list all assigning and trip from database
            var assignings = db.Assignings.Where(a => a.TripID == tripID).ToList();
            var tmpRequestIDs = new HashSet<int>();
            var requests = db.Requests.ToList();
            if (requests.Count == 0) return null;
            //add all request asigned to current trip
            foreach (var assigning in assignings)
            {
                tmpRequestIDs.Add(assigning.RequestID);
            }
            return (from tmpRequestID in tmpRequestIDs
                    from request in requests
                    where request.RequestID == tmpRequestID
                    select request).ToList();
        }

        // Update assigned requests of departed trip after click button 'Xe đã chạy'
        public void DepartedRequest(int[] tripsArray, int stationID)
        {
            foreach (var tID in tripsArray)
            {
                var trip = db.Trips.Single(t => t.TripID == tID);
                var listRequest = FindRequestAssignToTrip(tID);
                // update real departed time and status of trip
                trip.RealDepartureTime = DateTime.Now;
                trip.Status = "Đang chạy";
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                if (listRequest == null) return;
                //update the status of all request in the list
                foreach (var request in listRequest)
                {
                    if (request.DeliveryStatusID == 3) request.DeliveryStatusID = 4;
                    db.Entry(request).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        // Update assigned requests of arrived trip after click button 'Xe đã đến trạm'
        public void ArrivedRequest(int[] tripsArray, int stationID)
        {
            foreach (var tID in tripsArray)
            {
                var trip = db.Trips.Single(t => t.TripID == tID);

                // update real arrival time of trip
                if (FindStageFromRoute(trip.Schedule.RouteID).Last().EndPoint == stationID)
                {
                    trip.RealArrivalTime = DateTime.Now;
                    trip.Status = "Đã đến trạm";
                    db.Entry(trip).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    trip.Status = "Đã đến trạm";
                    db.SaveChanges();
                }
                //update for each request
                var listRequest = FindRequestAssignToTrip(tID);
                if (listRequest == null) return;
                var requests = new List<Request>();
                foreach (var request in listRequest)
                {
                    if (request.ToLocation == stationID)
                    {
                        request.DeliveryStatusID = 5;
                        request.ArrivedDate = DateTime.Now;
                        db.Entry(request).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    if (request.FromLocation == stationID)
                    {
                        request.DeliveryStatusID = 4;
                        db.Entry(request).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    if (DateTime.Now > trip.EstimateArrivalTime)
                    {
                        var assign = db.Assignings.Where(i => i.RequestID == request.RequestID).ToList();
                        int index = 0;
                        foreach (var tmp in assign)
                        {
                            if (tmp.TripID == tID) index = tmp.IndicateOrder;
                        }
                        if (index < assign.Count)
                        {
                            requests.Add(request);
                        }
                    }
                }
                foreach (var request in requests)
                {
                    var list = db.Assignings.Where(i => i.RequestID == request.RequestID).ToList();
                    foreach (var assigning in list)
                    {
                        db.Assignings.Remove(assigning);
                    }
                    request.FromLocation = stationID;
                    db.SaveChanges();
                }
                Assigning(requests);
            }
        }

        public ActionResult OtherStart(int stationId)
        {
            var trips = db.Trips.ToList();
            trips = Filter2(trips, stationId, false);

            return PartialView(trips);
        }

    }
}