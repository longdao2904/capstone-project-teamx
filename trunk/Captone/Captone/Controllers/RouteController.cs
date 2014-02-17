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
    public class RouteController : Controller
    {
        private iDeliverEntities db = new iDeliverEntities();

        //
        // GET: /Route/

        public ActionResult Index()
        {
            var routes = db.Routes.Include(r => r.Station).Include(r => r.Station1);
            //var routes = db.Routes.ToList();
            return View(routes.ToList());
        }

        //
        // GET: /Route/Details/5

        public ActionResult Details(string id = null)
        {
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        //
        // GET: /Route/Create

        public ActionResult Create()
        {
            ViewBag.StartPoint = new SelectList(db.Stations, "StationID", "StationName");
            ViewBag.EndPoint = new SelectList(db.Stations, "StationID", "StationName");
            ViewBag.CoachID = new SelectList(db.Coaches, "CoachID", "NumberPlate");
            return View();
        }

        //
        // POST: /Route/Create

        [HttpPost]
        public ActionResult Create(Route route)
        {
            if (ModelState.IsValid)
            {
                db.Routes.Add(route);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StartPoint = new SelectList(db.Stations, "StationID", "StationName", route.StartPoint);
            ViewBag.EndPoint = new SelectList(db.Stations, "StationID", "StationName", route.EndPoint);
            ViewBag.CoachID = new SelectList(db.Coaches, "CoachID", "NumberPlate");
            return View(route);
        }

        //
        // GET: /Route/Edit/5

        public ActionResult Edit(string id = null)
        {
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            ViewBag.StartPoint = new SelectList(db.Stations, "StationID", "StationName", route.StartPoint);
            ViewBag.EndPoint = new SelectList(db.Stations, "StationID", "StationName", route.EndPoint);
            return View(route);
        }

        //
        // POST: /Route/Edit/5

        [HttpPost]
        public ActionResult Edit(Route route)
        {
            if (ModelState.IsValid)
            {
                db.Entry(route).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StartPoint = new SelectList(db.Stations, "StationID", "StationName", route.StartPoint);
            ViewBag.EndPoint = new SelectList(db.Stations, "StationID", "StationName", route.EndPoint);
            return View(route);
        }

        //
        // GET: /Route/Delete/5

        public ActionResult Delete(string id = null)
        {
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        //
        // POST: /Route/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Route route = db.Routes.Find(id);
            db.Routes.Remove(route);
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