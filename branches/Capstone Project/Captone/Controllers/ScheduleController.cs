using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Captone.Models;

namespace Captone.Controllers
{
    public class ScheduleController : Controller
    {
        private iDeliverEntities db = new iDeliverEntities();

        #region CRUD
        //
        // GET: /Schedule/

        public ActionResult Index()
        {
            var schedules = db.Schedules;
            return View(schedules.ToList());
        }

        //
        // GET: /Schedule/Details/5

        public ActionResult Details(int id = 0)
        {
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        //
        // GET: /Schedule/Create

        public ActionResult Create()
        {
          
            ViewData["RouteID"] = new SelectList(db.Routes, "RouteID", "RouteName");
            return View();
        }

        //
        // POST: /Schedule/Create

        [HttpPost]
        public ActionResult Create(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["RouteID"] = new SelectList(db.Routes, "RouteID", "RouteName");
           
            return View(schedule);
        }

        //
        // GET: /Schedule/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            ViewData["RouteID"] = new SelectList(db.Routes, "RouteID", "RouteName");
 
            return View(schedule);
        }

        //
        // POST: /Schedule/Edit/5

        [HttpPost]
        public ActionResult Edit(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["RouteID"] = new SelectList(db.Routes, "RouteID", "RouteName");
            
            return View(schedule);
        }

        //
        // GET: /Schedule/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        //
        // POST: /Schedule/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        #endregion

        // GET: /SchedulePartialView

        public ActionResult SchedulePartialView(int routeID)
        {
            var schedules = new List<Schedule>();
            if (routeID != 0)
            {
                schedules = db.Schedules.Include(s => s.Coach).Include(s => s.Route).Where(s => s.Route.RouteID == routeID).ToList();
            }
            else
            {
                schedules = db.Schedules.Include(s => s.Coach).Include(s => s.Route).ToList();
            }
            return View(schedules);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetArrangedCoach(int routeID)
        {
            List<Coach> coaches = new List<Coach>();
            var coachIDs = (from ca in db.Schedules
                            where ca.RouteID == routeID
                            select ca.CoachID).ToList();
            foreach (var id in coachIDs)
            {
                var coach = db.Coaches.Where(c => c.CoachID == id).FirstOrDefault();
                coaches.Add(coach);
            }
            return Json(coaches.Select(c => new { c.CoachID, c.NumberPlate }), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetArrangeID(int routeID, int coachID)
        {
            var arranger = db.Schedules.Where(ca => ca.CoachID == coachID && ca.RouteID == routeID).ToList();
            return Json(arranger.Select(a => new { a.ScheduleID }), JsonRequestBehavior.AllowGet);
        }
    }
}