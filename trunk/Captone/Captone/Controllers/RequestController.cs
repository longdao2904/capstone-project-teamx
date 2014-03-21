using System.Web.Services;
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
        private readonly iDeliverEntities _db = new iDeliverEntities();

        #region CRUD
        //
        // GET: /Request/

        public ActionResult Index()
        {

            ViewBag.Status = _db.DeliveryStatus.ToList();
            var requests = _db.Requests.Include(r => r.Account).Include(r => r.DeliveryStatu).Include(r => r.ManageFee).Include(r => r.Station).Include(r => r.Station1);
            return View(requests.ToList());
        }

        public ActionResult ListRequest(int sttID)
        {
            if (sttID == 1)
            {
                var request = _db.Requests.Where(r => r.DeliveryStatusID == sttID);
                return PartialView("ListRequest", request.ToList());
            }
            else if (sttID == 2)
            {
                var request = _db.Requests.Where(r => r.DeliveryStatusID == sttID);
                return PartialView("ListRequestID2", request.ToList());
            }
            else if (sttID == 4)
            {
                var request = _db.Requests.Where(r => r.DeliveryStatusID == sttID);
                ViewBag.Transitted = request.ToList();
                return PartialView("ListRequestID4");
            }
            else if (sttID == 5)
            {
                var request = _db.Requests.Where(r => r.DeliveryStatusID == sttID);
                ViewBag.Arrived = request.ToList();
                return PartialView("ListRequestID5");
            }
            else if (sttID == 6)
            {
                var request = _db.Requests.Where(r => r.DeliveryStatusID == sttID);
                ViewBag.Delivered = request.ToList();
                return PartialView("ListRequestID6");
            }
            else if (sttID == 7)
            {
                var request = _db.Requests.Where(r => r.DeliveryStatusID == sttID);
                ViewBag.Cancelled = request.ToList();
                return PartialView("ListRequestID7");
            }
            var finalRequest = _db.Requests.Where(r => r.DeliveryStatusID == sttID);
            ViewBag.Expired = finalRequest.ToList();
            return PartialView("ListRequestID8");
        }

        //
        // GET: /Request/Details/5

        public ActionResult Details(int id = 0)
        {
            Request request = _db.Requests.Find(id);
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
            Request request = _db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.Username = new SelectList(_db.Accounts, "Username", "Password", request.Username);
            ViewBag.DeliveryStatusID = new SelectList(_db.DeliveryStatus, "DeliveryStatusID", "StatusName", request.DeliveryStatusID);
            ViewBag.FeeID = new SelectList(_db.ManageFees, "FeeID", "Fee", request.FeeID);
            ViewBag.FromLocation = new SelectList(_db.Stations, "StationID", "StationName", request.FromLocation);
            ViewBag.ToLocation = new SelectList(_db.Stations, "StationID", "StationName", request.ToLocation);
            return View(request);
        }


        //POST: /Request/Edit/5

        [HttpPost]
        public ActionResult Edit(Request request)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(request).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Username = new SelectList(_db.Accounts, "Username", "Password", request.Username);
            ViewBag.DeliveryStatusID = new SelectList(_db.DeliveryStatus, "DeliveryStatusID", "StatusName", request.DeliveryStatusID);
            ViewBag.FeeID = new SelectList(_db.ManageFees, "FeeID", "Fee", request.FeeID);
            ViewBag.FromLocation = new SelectList(_db.Stations, "StationID", "StationName", request.FromLocation);
            ViewBag.ToLocation = new SelectList(_db.Stations, "StationID", "StationName", request.ToLocation);
            return View(request);
        }


        //
        // GET: /Request/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Request request = _db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = _db.Requests.Find(id);
            _db.Requests.Remove(request);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        #region Update request status and prepare for return
        public Boolean ChangeStatus(int requestID)
        {
            Request request = _db.Requests.Single(p => p.RequestID == requestID);
            if (request == null)
            {
                return false;
            }
            request.DeliveryStatusID = 2;
            _db.SaveChanges();
            return true;
        }

        // Update request status
        public Boolean UpdateStatus(List<int> requestIDs)
        {
            if (requestIDs != null)
            {
                foreach (var id in requestIDs)
                {
                    Request rq = _db.Requests.FirstOrDefault(r => r.RequestID == id);
                    if (rq != null && rq.DeliveryStatusID == 4)
                    {
                        rq.DeliveryStatusID = 5;
                        rq.ArrivedDate = DateTime.Now.Date;
                    }
                    else if (rq != null && rq.DeliveryStatusID == 5)
                    {
                        rq.DeliveryStatusID = 6;
                    }
                    _db.SaveChanges();
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
                    Request rq = _db.Requests.FirstOrDefault(r => r.RequestID == id);
                    if (rq != null)
                    {
                        if (rq.ArrivedDate != null)
                        {
                            var arrivedDate = (DateTime)rq.ArrivedDate;
                            DateTime currentDate = DateTime.Now.Date;
                            TimeSpan waitTime = currentDate - arrivedDate;
                            if (waitTime.TotalDays >= 2 && rq.DeliveryStatusID == 5)
                            {
                                rq.DeliveryStatusID = 8;
                                rq.Type = false;
                                _db.SaveChanges();
                                return true;
                            }
                        }
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
                    Request rq = _db.Requests.FirstOrDefault(r => r.RequestID == id);
                    Invoice inv = _db.Invoices.FirstOrDefault(i => i.RequestID == id);
                    if (rq != null)
                    {
                        if (inv != null) inv.Price = rq.ManageFee.Fee * 0.8;
                        var oldStation = rq.FromLocation;
                        rq.FromLocation = rq.ToLocation;
                        rq.ToLocation = oldStation;
                        rq.DeliveryStatusID = 2;
                        rq.DateRequest = DateTime.Now.Date;
                    }
                    _db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}