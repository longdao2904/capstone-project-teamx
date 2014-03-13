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

        //
        // GET: /Schedule/

        public ActionResult Index()
        {
            if (Session["USERNAME"] == null)
            {
                return RedirectToAction("LogOn", "Account");
            }
            else
            {
                var schedules = db.Schedules.Include(s => s.Coach).Include(s => s.Route);
                return View(schedules.ToList());
            }

        }

        //
        // GET: /SchedulePartialView

        public ActionResult SchedulePartialView(int routeID)
        {
            var schedules = new List<Schedule>();
            if (routeID != 0)
            {
                schedules = db.Schedules.Include(s => s.Coach).Include(s => s.Route).Where(s => s.RouteID == routeID).ToList();
            }
            else
            {
                schedules = db.Schedules.Include(s => s.Coach).Include(s => s.Route).ToList();
            }
            return View(schedules);
        }

        //
        // GET: /Schedule/Details/5

        public ActionResult Details(int id = 0)
        {
            if (Session["USERNAME"] == null)
            {
                return RedirectToAction("LogOn", "Account");
            }
            else
            {
                Schedule schedule = db.Schedules.Find(id);
                if (schedule == null)
                {
                    return HttpNotFound();
                }
                return View(schedule);
            }

        }

        //
        // GET: /Schedule/Create

        public ActionResult Create()
        {
            if (Session["USERNAME"] == null)
            {
                return RedirectToAction("LogOn", "Account");
            }
            else
            {
                ViewBag.CoachID = new SelectList(db.Coaches, "CoachID", "NumberPlate");
                ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName");
                return View();
            }

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

            ViewBag.CoachID = new SelectList(db.Coaches, "CoachID", "NumberPlate", schedule.CoachID);
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName", schedule.RouteID);
            return View(schedule);
        }

        //
        // GET: /Schedule/Edit/5

        public ActionResult Edit(int id = 0)
        {
            if (Session["USERNAME"] == null)
            {
                return RedirectToAction("LogOn", "Account");
            }
            else
            {
                Schedule schedule = db.Schedules.Find(id);
                if (schedule == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CoachID = new SelectList(db.Coaches, "CoachID", "NumberPlate", schedule.CoachID);
                ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName", schedule.RouteID);
                return View(schedule);
            }

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
            ViewBag.CoachID = new SelectList(db.Coaches, "CoachID", "NumberPlate", schedule.CoachID);
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName", schedule.RouteID);
            return View(schedule);
        }

        //
        // GET: /Schedule/Delete/5

        public ActionResult Delete(int id = 0)
        {
            if (Session["USERNAME"] == null)
            {
                return RedirectToAction("LogOn", "Account");
            }
            else
            {
                Schedule schedule = db.Schedules.Find(id);
                if (schedule == null)
                {
                    return HttpNotFound();
                }
                return View(schedule);
            }

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
    }
}