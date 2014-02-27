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
    public class FeeController : Controller
    {
        private iDeliverEntities db = new iDeliverEntities();

        //
        // GET: /Coach/

        public ActionResult Index()
        {
            var fee = db.ManageFees.Include(c => c.Fee);
            return View(fee.ToList());
        }

        //
        // GET: /Coach/Details/5

        public ActionResult Details(int id = 0)
        {
            ManageFee manageFee = db.ManageFees.Find(id);
            if (manageFee == null)
            {
                return HttpNotFound();
            }
            return View(manageFee);
        }

        //
        // GET: /Coach/Create

        public ActionResult Create()
        {
            ViewBag.ManageFeesID = new SelectList(db.ManageFees, "CoachTypeID", "Seats");
            return View();
        }

        //
        // POST: /Coach/Create

        [HttpPost]
        public ActionResult Create(ManageFee manageFee)
        {
            if (ModelState.IsValid)
            {
                db.ManageFees.Add(manageFee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManageFeesID = new SelectList(db.ManageFees, "CoachType", "Seats", manageFee.FeeID);
            return View(manageFee);
        }

        //
        // GET: /Coach/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            ViewBag.CoachTypeID = new SelectList(db.CoachTypes, "CoachTypeID", "CoachTypeID", coach.CoachTypeID);
            return View(coach);
        }

        //
        // POST: /Coach/Edit/5

        [HttpPost]
        public ActionResult Edit(Coach coach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CoachTypeID = new SelectList(db.CoachTypes, "CoachTypeID", "CoachTypeID", coach.CoachTypeID);
            return View(coach);
        }

        //
        // GET: /Coach/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        //
        // POST: /Coach/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Coach coach = db.Coaches.Find(id);
            db.Coaches.Remove(coach);
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