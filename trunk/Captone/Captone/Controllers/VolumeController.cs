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
    public class VolumeController : Controller
    {
        private iDeliverEntities db = new iDeliverEntities();

        //
        // GET: /Volume/

        public ActionResult Index()
        {
            var volumes = db.Volumes.Include(v => v.CoachType).Include(v => v.Route);
            return View(volumes.ToList());
        }

        //
        // GET: /Volume/Details/5

        public ActionResult Details(int id = 0)
        {
            Volume volume = db.Volumes.Find(id);
            if (volume == null)
            {
                return HttpNotFound();
            }
            return View(volume);
        }

        //
        // GET: /Volume/Create

        public ActionResult Create()
        {
            ViewBag.CoachTypeID = new SelectList(db.CoachTypes, "CoachTypeID", "Seats");
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName");
            return View();
        }

        //
        // POST: /Volume/Create

        [HttpPost]
        public ActionResult Create(Volume volume)
        {
            if (ModelState.IsValid)
            {
                db.Volumes.Add(volume);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CoachTypeID = new SelectList(db.CoachTypes, "CoachTypeID", "Seats", volume.CoachTypeID);
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName", volume.RouteID);
            return View(volume);
        }

        //
        // GET: /Volume/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Volume volume = db.Volumes.Find(id);
            if (volume == null)
            {
                return HttpNotFound();
            }
            ViewBag.CoachTypeID = new SelectList(db.CoachTypes, "CoachTypeID", "Seats", volume.CoachTypeID);
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName", volume.RouteID);
            return View(volume);
        }

        //
        // POST: /Volume/Edit/5

        [HttpPost]
        public ActionResult Edit(Volume volume)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volume).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CoachTypeID = new SelectList(db.CoachTypes, "CoachTypeID", "CoachTypeID", volume.CoachTypeID);
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName", volume.RouteID);
            return View(volume);
        }

        //
        // GET: /Volume/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Volume volume = db.Volumes.Find(id);
            if (volume == null)
            {
                return HttpNotFound();
            }
            return View(volume);
        }

        //
        // POST: /Volume/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Volume volume = db.Volumes.Find(id);
            db.Volumes.Remove(volume);
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