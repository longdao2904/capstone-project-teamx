using System.Web.Services;
using Captone.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Captone.Repositories;
using Captone.Repositories.Interfaces;
using Captone.Services;
using Captone.Services.Interfaces;
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

        public ActionResult ListRequest()
        {
            var request = _db.Requests.Where(r => r.DeliveryStatusID == 2);
            return View(request.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            Request request = _db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

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

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
        #endregion

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
                        _db.SaveChanges();
                    }
                    else if (rq != null && rq.DeliveryStatusID == 5)
                    {
                        rq.DeliveryStatusID = 6;
                        rq.Type = true;

                        _db.SaveChanges();
                    }
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
                    if (rq != null && rq.Type == false)
                    {
                        if (inv != null) inv.Price = rq.ManageFee.Fee * 0.8;
                        var oldStation = rq.FromLocation;
                        rq.FromLocation = rq.ToLocation;
                        rq.ToLocation = oldStation;
                        rq.DeliveryStatusID = 2;
                        rq.DateRequest = DateTime.Now.Date;
                        rq.ArrivedDate = null;
                    }
                    _db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        #endregion
        //
        public ActionResult AcceptRequest(int stationID)
        {
            var list = _db.Requests.Where(p => p.FromLocation == stationID & p.DeliveryStatusID == 1).ToList();
            return View(list);
        }
        //
        public ActionResult InputInvoice(int RequestID)
        {
            var reqest = _db.Requests.Where(p => p.RequestID == RequestID).Single();
            return PartialView(reqest);
        }
        //
        public void PaymentInProcess(int requestID)
        {
            var request = _db.Requests.Single(p => p.RequestID == requestID);
            request.DeliveryStatusID = 10;
            _db.SaveChanges();
        }
        //
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
        //
        public ActionResult LatePayment(int stationID)
        {
            var request = _db.Requests.Where(p => p.FromLocation == stationID & p.DeliveryStatusID == 9).ToList();
            return View(request);
        }
        //Create PDF
        [HttpPost]
        public Boolean CreatePdf(int invoiceID)
        {
            var invoice = _db.Invoices.Where(i => i.InvoiceID == invoiceID).Single();

            List<string> str = new List<string>();
            string htmlText1 = "HOÁ ĐƠN KHÁCH HÀNG";
            string htmlText2 = "Tên khách hàng: " + invoice.Request.Username;
            string htmlText3 = "Mã hàng: " + invoice.Request.RequestCode;
            string htmlText4 = "Gửi từ: " + invoice.Request.SenderAddress;
            string htmlText5 = "Đến: " + invoice.Request.ReceiverAddress;
            string htmlText6 = "Khối lượng hàng hoá:" + invoice.Weight;
            string htmlText7 = "Thể tích hàng hoá: " + invoice.Volume;
            string htmlText8 = "Thành tiền: " + invoice.Price;
            str.Add(htmlText1);
            str.Add(htmlText2);
            str.Add(htmlText3);
            str.Add(htmlText4);
            str.Add(htmlText5);
            str.Add(htmlText6);
            str.Add(htmlText7);
            str.Add(htmlText8);
            HTMLToPdf(str, "PDFfile.pdf");
            return true;
        }
        //extend the time for payment of request, means updating the date request to current day
        public void UpdateDatePostForRequest(int requestID)
        {
            var request = _db.Requests.Single(p => p.RequestID == requestID);
            request.DateRequest = DateTime.Now;
            request.DeliveryStatusID = 1;
            _db.SaveChanges();
        }
        //
        public void HTMLToPdf(List<string> HTML, string FilePath)
        {
            string FONT = "c:/windows/fonts/arialbd.ttf";
            using (Document document = new Document())
            {
                string path = Server.MapPath("~/");
                PdfWriter.GetInstance(document, new FileStream(path + "/Invoice.pdf", FileMode.Create));
                document.Open();
                BaseFont bf = BaseFont.CreateFont(
                    FONT, BaseFont.IDENTITY_H, BaseFont.EMBEDDED
                    );

                StyleSheet styles = new StyleSheet();
                HTMLWorker hw = new HTMLWorker(document);
                for (var i = 0; i < HTML.Count; i++)
                {

                    document.Add(new Paragraph(HTML[i], new Font(bf, 12)));
                }

                document.Close();
            }
        }

        //
        //public FileResult DisplayPDF()
        //{
        //    string path = Server.MapPath("~/");
        //    return File(path + "\\Invoice.pdf", "application/pdf");
        //}
        //  public double CalculatePrice(float weight, float volume, int FromLocation, int ToLocation)
        //{

        //    var list = _db.Routes.Where(p => p.StartPoint == FromLocation && p.EndPoint == ToLocation).Single();

        //    var getPrice = _db.ManageFees.ToList();
        //    double priceVolume = 0;
        //    for (var i = 0; i < getPrice.Count; i++)
        //    {
        //        if ((weight > getPrice[i].MinWeight && weight < getPrice[i].MaxWeight) && (volume > getPrice[i].MinVolume && volume < getPrice[i].MaxVolume))
        //        {
        //            priceVolume = getPrice[i].Fee;
        //        }
        //        else if (weight > getPrice[i].MaxWeight && (volume > getPrice[i].MinVolume && volume < getPrice[i].MaxVolume))
        //        {
        //            priceVolume = (weight / getPrice[i].MaxWeight) * getPrice[i].Fee;
        //        }
        //        else if (volume > getPrice[i].MaxVolume)
        //        {
        //            priceVolume = (volume / getPrice[i].MaxVolume) * getPrice[i].Fee;
        //        }
        //    }
        //    return (priceVolume + list.Price.Value);
        //}
    }
}