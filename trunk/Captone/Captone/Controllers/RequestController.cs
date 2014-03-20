using Captone.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Captone.Controllers
{
    public class RequestController : Controller
    {
        private iDeliverEntities db = new iDeliverEntities();

        #region CRUD
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
            var request = db.Requests.Where(r => r.DeliveryStatusID == sttID);
            return PartialView("ListRequest", request.ToList());
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
            ViewBag.FeeID = new SelectList(db.ManageFees, "FeeID", "Fee", request.FeeID);
            ViewBag.FromLocation = new SelectList(db.Stations, "StationID", "StationName", request.FromLocation);
            ViewBag.ToLocation = new SelectList(db.Stations, "StationID", "StationName", request.ToLocation);
            return View(request);
        }


        //POST: /Request/Edit/5

        [HttpPost]
        public ActionResult Edit(Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Username = new SelectList(db.Accounts, "Username", "Password", request.Username);
            ViewBag.DeliveryStatusID = new SelectList(db.DeliveryStatus, "DeliveryStatusID", "StatusName", request.DeliveryStatusID);
            ViewBag.FeeID = new SelectList(db.ManageFees, "FeeID", "Fee", request.FeeID);
            ViewBag.FromLocation = new SelectList(db.Stations, "StationID", "StationName", request.FromLocation);
            ViewBag.ToLocation = new SelectList(db.Stations, "StationID", "StationName", request.ToLocation);
            return View(request);
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

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public Boolean ChangeStatus(int requestID)
        {
            Request request = db.Requests.Where(p => p.RequestID == requestID).Single();
            if (request != null)
            {
                request.DeliveryStatusID = 2;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        // Update request status
        public Boolean UpdateStatus(List<int> requestIDs)
        {
            if (requestIDs != null)
            {
                foreach (var id in requestIDs)
                {
                    Request rq = db.Requests.Where(r => r.RequestID == id).FirstOrDefault();
                    if (rq.DeliveryStatusID == 4)
                    {
                        rq.DeliveryStatusID = 5;
                        rq.ArrivedDate = DateTime.Now.Date;
                    }
                    else if (rq.DeliveryStatusID == 5)
                    {
                        rq.DeliveryStatusID = 6;
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        // Auto set request status to 'Đã hết hạn - 8' if delivery date was late 2 days
        public Boolean AutoSet(List<int> requestIDs)
        {
            if (requestIDs != null)
            {
                foreach (var id in requestIDs)
                {
                    Request rq = db.Requests.Where(r => r.RequestID == id).FirstOrDefault();
                    DateTime arrivedDate = (DateTime) rq.ArrivedDate;
                    DateTime currentDate = DateTime.Now.Date;
                    TimeSpan waitTime = currentDate - arrivedDate;
                    if (waitTime.TotalDays == 0 && rq.DeliveryStatusID == 5)
                    {
                        rq.DeliveryStatusID = 8;
                        rq.Type = false;
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        // Prepare late delivery request for returning
        public Boolean Return(List<int> listRequest)
        {
            if (listRequest != null)
            {
                foreach (var id in listRequest)
                {
                    Request rq = db.Requests.Where(r => r.RequestID == id).FirstOrDefault();
                    Invoice inv = db.Invoices.Where(i => i.RequestID == id).FirstOrDefault();
                    inv.Price = rq.ManageFee.Fee * 0.8;
                    int oldStation = rq.FromLocation;
                    rq.FromLocation = rq.ToLocation;
                    rq.ToLocation = oldStation;
                    rq.DeliveryStatusID = 2;
                    rq.DateRequest = DateTime.Now.Date;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}