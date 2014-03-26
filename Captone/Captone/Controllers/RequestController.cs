﻿using System.Web.Services;
using Captone.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

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

        public ActionResult Delete(int id)
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
        // Change status from 1 to 2 - Wating to Approved
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
                        rq.Type = true;
                    }
                    _db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
       
        [HttpPost]
        public Boolean UpdateStatusAfterCheckOut(int requestId)
        {
            Request request = _db.Requests.Where(r => r.RequestID == requestId).FirstOrDefault();
            request.Payment = true;
            _db.SaveChanges();
            return true;
        }

        // Auto set request status to 'Đã hết hạn - 8' if delivery date was late 2 days
        [HttpPost]
        [WebMethod]
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


        public ActionResult AcceptRequest(int stationID)
        {
            var list = _db.Requests.Where(p => p.FromLocation == stationID & p.DeliveryStatusID == 1).ToList();
            return View(list);
        }
        public ActionResult InputInvoice(int RequestID)
        {
            var reqest = _db.Requests.Where(p => p.RequestID == RequestID).Single();
            return PartialView(reqest);
        }
        public void InsertInvoice(int RequestID, float Weight, float Volume, float Price)
        {
            Invoice invoice = new Invoice();
            invoice.RequestID = RequestID;
            invoice.Weight = Weight;
            invoice.Volume = Volume;
            invoice.Price = Price;
            var request = _db.Requests.Where(p => p.RequestID == RequestID).Single();
            request.DeliveryStatusID = 2;
            _db.Invoices.Add(invoice);
            _db.SaveChanges();
        }
        public ActionResult LatePayment(int stationID)
        {
            var list = _db.Requests.Where(p => p.FromLocation == stationID & p.DeliveryStatusID == 8).ToList();
            return View(list);           
        }
        //Create PDF
        [HttpPost]
        public String CreatePdf(String Weight, String Volume, String Price, int RequestID)
        {
            var request = _db.Requests.Where(p => p.RequestID == RequestID).Single();

            string htmlText = "<div>" + "Hoa don khach hang<br>=====================================<br><br>"+
                "Ten khach hang: " + request.Username + "<br>" +
                "Gui tu: " + request.SenderAddress + " den: "+request.ReceiverAddress + "<br>" +
                "Khoi luong hang hoa:" + Weight + "<br>" +
                "The tich hang hoa: " + Volume + "<br>" +

                "-------------------------------<br>Thanh tien: " +Price+
                "<br><br>=====================================<br>" +
          "</div>";
            
            HTMLToPdf(htmlText, "PDFfile.pdf");
            return "true";
        }
        public void HTMLToPdf(string HTML, string FilePath)
        {

            Document document = new Document();
            string path = Server.MapPath("~/");
            PdfWriter.GetInstance(document, new FileStream(path + "/Invoice.pdf", FileMode.Create));

            document.Open();

            StyleSheet styles = new StyleSheet();
            HTMLWorker hw = new HTMLWorker(document);
            hw.Parse(new StringReader(HTML));
            document.Close();

        }

        public FileResult DisplayPDF()
        {
            string path = Server.MapPath("~/");
            return File(path+"\\Invoice.pdf", "application/pdf");
        }
    }
}