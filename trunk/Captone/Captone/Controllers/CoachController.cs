using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Captone.Models;
using Captone.Services.Interfaces;

namespace Captone.Controllers
{
    public class CoachController : Controller
    {
        //
        // GET: /Coach/
        private readonly iDeliverEntities db = new iDeliverEntities();
        private readonly ICoachService _coachService;
        public CoachController(ICoachService coachService)
        {
            _coachService = coachService;
        }

        public ActionResult Index()
        {
            return View(_coachService.GetAllCoaches());
        }
        //
        // GET: /Coach/Details/5

        public ActionResult Details(int id = 0)
        {
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        //
        // GET: /Coach/Create

        public ActionResult Create()
        {
            ViewBag.CoachTypeID = new SelectList(db.CoachTypes, "CoachTypeID", "Seats");
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName");
            return View();
        }

        //
        // POST: /Coach/Create

        [HttpPost]
        public ActionResult Create(Coach coach)
        {
            if (ModelState.IsValid)
            {
                db.Coaches.Add(coach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CoachTypeID = new SelectList(db.CoachTypes, "CoachTypeID", "Seats", coach.CoachTypeID);
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName", coach.RouteID);
            return View(coach);
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
            ViewBag.CoachTypeID = new SelectList(db.CoachTypes, "CoachTypeID", "Seats", coach.CoachTypeID);
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName", coach.RouteID);
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
            ViewBag.RouteID = new SelectList(db.Routes, "RouteID", "RouteName", coach.RouteID);
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