using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Captone.Models;
using Captone.Services;
using Newtonsoft.Json;
namespace Captone.Controllers
{
    public class TripController : Controller
    {
        private iDeliverEntities db = new iDeliverEntities();

        //
        // GET: /Trip/

        public ActionResult Index()
        {
            var trips = db.Trips;
            return View(trips.ToList());
        }

        //
        // POST: /Trip/ListTrip

        public ActionResult ListTrip(DateTime tripDate)
        {
            var trips = db.Trips.Where(t => t.Date == tripDate);
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

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Trip/Create

        [HttpPost]
        public ActionResult Create(Trip trip)
        {
            if (ModelState.IsValid)
            {
                
                db.Trips.Add(trip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        //
        // GET: /Trip/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        //
        // POST: /Trip/Edit/5

        [HttpPost]
        public ActionResult Edit(Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        //
        // GET: /Trip/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        //
        // POST: /Trip/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Trip trip = db.Trips.Find(id);
            db.Trips.Remove(trip);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult ListTripForRequest()
        {
            var listTrip = db.Trips.ToList();
            return PartialView("ListTripForRequest",listTrip);
        }
  
        [HttpPost]
        [WebMethod]
        public ActionResult Assigning(List<Request> request)
        {
            AssigningService assign = new AssigningService();
           var result = assign.Assigning(request, DateTime.Now);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}