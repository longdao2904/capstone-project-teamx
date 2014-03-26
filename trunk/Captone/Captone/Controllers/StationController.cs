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
    public class StationController : Controller
    {
        private iDeliverEntities db = new iDeliverEntities();

        //
        // GET: /Station/

        public ActionResult Index()
        {
            return View(db.Stations.ToList());
        }

        //
        // GET: /Station/Details/5

        public ActionResult Details(int id = 0)
        {
            Station station = db.Stations.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }

        //
        // GET: /Station/Create

        public ActionResult Create()
        {
            ViewBag.ProvinceID = new SelectList(db.Provinces, "ProvinceID", "ProvinceName");
            return View();
        }

        //
        // POST: /Station/Create

        [HttpPost]
        public ActionResult Create(Station station)
        {
            ViewBag.ProvinceID = new SelectList(db.Provinces, "ProvinceID", "ProvinceName", station.ProvinceID);
            if (ModelState.IsValid)
            {
                db.Stations.Add(station);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(station);
        }

        //
        // GET: /Station/Edit/5
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            Station station = db.Stations.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProvinceID = new SelectList(db.Provinces, "ProvinceID", "ProvinceName", station.ProvinceID);
            return View(station);
        }

        //
        // POST: /Station/Edit/5

        [HttpPost]
        public ActionResult Edit(Station station)
        {
            if (ModelState.IsValid)
            {
                db.Entry(station).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(station);
        }

        //
        // GET: /Station/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Station station = db.Stations.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }

        //
        // POST: /Station/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Station station = db.Stations.Find(id);
            db.Stations.Remove(station);
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