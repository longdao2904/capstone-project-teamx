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
    public class RequestController : Controller
    {
        private iDeliverEntities db = new iDeliverEntities();

        //
        // GET: /Request/

        public ActionResult Index()
        {
            
            ViewBag.Status = db.DeliveryStatus.ToList();
            var requests = db.Requests.Include(r => r.Account).Include(r => r.DeliveryStatu).Include(r => r.ManageFee).Include(r => r.Station).Include(r => r.Station1);
            return View(requests.ToList());
        }

        public ActionResult ListRequest(int sttID)
        {
            var request = db.Requests.Where(r => r.DeliveryStatusID == sttID).ToList();
            return PartialView("ListRequest", request);
        }

        //
        // GET: /Request/Details/5

        public ActionResult Details(int id = 0)
        {
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        //
        // GET: /Request/Create

        //public ActionResult Create()
        //{
        //    ViewBag.Username = new SelectList(db.Accounts, "Username", "Password");
        //    ViewBag.DeliveryStatusID = new SelectList(db.DeliveryStatus, "DeliveryStatusID", "StatusName");
        //    ViewBag.FeeID = new SelectList(db.ManageFees, "FeeID", "FeeID");
        //    ViewBag.FromLocation = new SelectList(db.Stations, "StationID", "StationName");
        //    ViewBag.ToLocation = new SelectList(db.Stations, "StationID", "StationName");
        //    return View();
        //}

        ////
        //// POST: /Request/Create

        //[HttpPost]
        //public ActionResult Create(Request request)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Requests.Add(request);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Username = new SelectList(db.Accounts, "Username", "Password", request.Username);
        //    ViewBag.DeliveryStatusID = new SelectList(db.DeliveryStatus, "DeliveryStatusID", "StatusName", request.DeliveryStatusID);
        //    ViewBag.FeeID = new SelectList(db.ManageFees, "FeeID", "FeeID", request.FeeID);
        //    ViewBag.FromLocation = new SelectList(db.Stations, "StationID", "StationName", request.FromLocation);
        //    ViewBag.ToLocation = new SelectList(db.Stations, "StationID", "StationName", request.ToLocation);
        //    return View(request);
        //}

        //
        // GET: /Request/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.Username = new SelectList(db.Accounts, "Username", "Password", request.Username);
            ViewBag.DeliveryStatusID = new SelectList(db.DeliveryStatus, "DeliveryStatusID", "StatusName", request.DeliveryStatusID);
            ViewBag.FeeID = new SelectList(db.ManageFees, "FeeID", "FeeID", request.FeeID);
            ViewBag.FromLocation = new SelectList(db.Stations, "StationID", "StationName", request.FromLocation);
            ViewBag.ToLocation = new SelectList(db.Stations, "StationID", "StationName", request.ToLocation);
            return View(request);
        }

        //
        // POST: /Request/Edit/5

        //[HttpPost]
        //public ActionResult Edit(Request request)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(request).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Username = new SelectList(db.Accounts, "Username", "Password", request.Username);
        //    ViewBag.DeliveryStatusID = new SelectList(db.DeliveryStatus, "DeliveryStatusID", "StatusName", request.DeliveryStatusID);
        //    ViewBag.FeeID = new SelectList(db.ManageFees, "FeeID", "FeeID", request.FeeID);
        //    ViewBag.FromLocation = new SelectList(db.Stations, "StationID", "StationName", request.FromLocation);
        //    ViewBag.ToLocation = new SelectList(db.Stations, "StationID", "StationName", request.ToLocation);
        //    return View(request);
        //}


        [HttpGet]
        public ActionResult EditPartialView(int id)
        {
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.Username = new SelectList(db.Accounts, "Username", "Password", request.Username);
            ViewBag.DeliveryStatusID = new SelectList(db.DeliveryStatus, "DeliveryStatusID", "StatusName", request.DeliveryStatusID);
            ViewBag.FeeID = new SelectList(db.ManageFees, "FeeID", "FeeID", request.FeeID);
            ViewBag.FromLocation = new SelectList(db.Stations, "StationID", "StationName", request.FromLocation);
            ViewBag.ToLocation = new SelectList(db.Stations, "StationID", "StationName", request.ToLocation);
            return PartialView("EditPartialView", request);
        }

        [HttpPost]
        public void Edit(Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
            }
        }




        //
        // GET: /Request/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        public ActionResult Assigning()
        {
            var requests = db.Requests.Include(r => r.Account).Include(r => r.DeliveryStatu).Include(r => r.ManageFee).Include(r => r.Station).Include(r => r.Station1);
            return View(requests.ToList());
        }
        //
        // POST: /Request/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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