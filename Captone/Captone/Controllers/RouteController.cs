using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Captone.Models;
using Captone.Services;

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
            return View(routes.ToList());
        }

        //
        // GET: /Route/Details/5

        public ActionResult Details(int id = 0)
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
            ViewBag.EndPoint = new SelectList(db.Stations, "StationID", "StationName");
            ViewBag.StartPoint = new SelectList(db.Stations, "StationID", "StationName");
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

            ViewBag.EndPoint = new SelectList(db.Stations, "StationID", "StationName", route.EndPoint);
            ViewBag.StartPoint = new SelectList(db.Stations, "StationID", "StationName", route.StartPoint);
            return View(route);
        }

        //
        // GET: /Route/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            ViewBag.EndPoint = new SelectList(db.Stations, "StationID", "StationName", route.EndPoint);
            ViewBag.StartPoint = new SelectList(db.Stations, "StationID", "StationName", route.StartPoint);
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
            ViewBag.EndPoint = new SelectList(db.Stations, "StationID", "StationName", route.EndPoint);
            ViewBag.StartPoint = new SelectList(db.Stations, "StationID", "StationName", route.StartPoint);
            return View(route);
        }

        //
        // GET: /Route/Delete/5

        public ActionResult Delete(int id = 0)
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
        public ActionResult DeleteConfirmed(int id)
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