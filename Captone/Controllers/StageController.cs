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
    public class StageController : Controller
    {
        private iDeliverEntities db = new iDeliverEntities();

        //
        // GET: /Stage/

        public ActionResult Index()
        {
            var stages = db.Stages.Include(s => s.Station).Include(s => s.Station1);
            return View(stages.ToList());
        }

        //
        // GET: /Stage/Details/5

        public ActionResult Details(int id = 0)
        {
            Stage stage = db.Stages.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }
            return View(stage);
        }

        //
        // GET: /Stage/Create

        public ActionResult Create()
        {
            ViewBag.EndPoint = new SelectList(db.Stations, "StationID", "StationName");
            ViewBag.StartPoint = new SelectList(db.Stations, "StationID", "StationName");
            return View();
        }

        //
        // POST: /Stage/Create

        [HttpPost]
        public ActionResult Create(Stage stage)
        {
            if (ModelState.IsValid)
            {
                db.Stages.Add(stage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EndPoint = new SelectList(db.Stations, "StationID", "StationName", stage.EndPoint);
            ViewBag.StartPoint = new SelectList(db.Stations, "StationID", "StationName", stage.StartPoint);
            return View(stage);
        }

        //
        // GET: /Stage/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Stage stage = db.Stages.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }
            ViewBag.EndPoint = new SelectList(db.Stations, "StationID", "StationName", stage.EndPoint);
            ViewBag.StartPoint = new SelectList(db.Stations, "StationID", "StationName", stage.StartPoint);
            return View(stage);
        }

        //
        // POST: /Stage/Edit/5

        [HttpPost]
        public ActionResult Edit(Stage stage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EndPoint = new SelectList(db.Stations, "StationID", "StationName", stage.EndPoint);
            ViewBag.StartPoint = new SelectList(db.Stations, "StationID", "StationName", stage.StartPoint);
            return View(stage);
        }

        //
        // GET: /Stage/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Stage stage = db.Stages.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }
            return View(stage);
        }

        //
        // POST: /Stage/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Stage stage = db.Stages.Find(id);
            db.Stages.Remove(stage);
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