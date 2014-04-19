using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Services;
using Captone.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

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

        public ActionResult ListRequest(int stationId)
        {
            var request = _db.Requests.Where(r => r.DeliveryStatusID == 2 & r.Station.StationID == stationId);
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
        //
        [HttpPost]
        public Boolean UpdateStatusAfterCheckOut(int requestId)
        {
            var request = _db.Requests.FirstOrDefault(r => r.RequestID == requestId);
            if (request != null) request.Payment = true;
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
        public Boolean Return(int listRequest)
        {
            if (listRequest != null)
            {

                Request rq = _db.Requests.FirstOrDefault(r => r.RequestID == listRequest);
                Invoice inv = _db.Invoices.FirstOrDefault(i => i.RequestID == listRequest);
                if (rq != null)
                {
                    if (inv != null) inv.Price = rq.ManageFee.Fee * 0.8;
                    var oldStation = rq.FromLocation;
                    rq.FromLocation = rq.ToLocation;
                    rq.ToLocation = oldStation;
                    rq.DeliveryStatusID = 2;
                    rq.Type = false;
                    rq.DateRequest = DateTime.Now.Date;
                    rq.ArrivedDate = null;
                }
                _db.SaveChanges();
                return true;

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
            if (request.TypeOfPayment == "Tiền mặt")
            {
                request.Payment = true;
            }
            _db.Invoices.Add(invoice);
            _db.SaveChanges();
        }
        //
        public ActionResult LatePayment(int stationID)
        {
            var request = _db.Requests.Where(p => p.FromLocation == stationID & p.DeliveryStatusID == 9).ToList();
            return View(request);
        }
        private string ReadGroup3(string G3)
        {
            string[] ReadDigit = new string[10] { " Không", " Một", " Hai", " Ba", " Bốn", " Năm", " Sáu", " Bảy", " Tám", " Chín" };
            string temp = "";
            if (G3 == "000") return "";

            //Đọc số hàng trăm
            temp = ReadDigit[int.Parse(G3[0].ToString())] + " Trăm";
            //Đọc số hàng chục
            if (G3[1].ToString() == "0")
                if (G3[2].ToString() == "0") return temp;
                else
                {
                    temp += " Lẻ" + ReadDigit[int.Parse(G3[2].ToString())];
                    return temp;
                }
            else
                temp += ReadDigit[int.Parse(G3[1].ToString())] + " Mươi";
            //--------------Đọc hàng đơn vị

            if (G3[2].ToString() == "5") temp += " Lăm";
            else if (G3[2].ToString() != "0") temp += ReadDigit[int.Parse(G3[2].ToString())];
            return temp;


        }
        public string ReadMoney(string Money)
        {
            string temp = "";
            // Cho đủ 12 số
            while (Money.Length < 12)
            {
                Money = "0" + Money;
            }
            string g1 = Money.Substring(0, 3);
            string g2 = Money.Substring(3, 3);
            string g3 = Money.Substring(6, 3);
            string g4 = Money.Substring(9, 3);

            //Đọc nhóm 1 ---------------------
            if (g1 != "000")
            {
                temp = ReadGroup3(g1);
                temp += " Tỷ";
            }
            //Đọc nhóm 2-----------------------
            if (g2 != "000")
            {
                temp += ReadGroup3(g2);
                temp += " Triệu";
            }
            //---------------------------------
            if (g3 != "000")
            {
                temp += ReadGroup3(g3);
                temp += " Ngàn";
            }
            //-----------------------------------
            //Chỗ này ko biết có nên ko ?
            //temp =temp + ReadGroup3(g4).Replace("Không Trăm Lẻ","Lẻ"); // Đọc 1000001 là Một Triệu Lẻ Một thay vì Một Triệu Không Trăm Lẻ 1;
            temp = temp + ReadGroup3(g4);
            //---------------------------------
            // Tinh chỉnh
            temp = temp.Replace("Một Mươi", "Mười");
            temp = temp.Trim();
            if (temp.IndexOf("Không Trăm") == 0)
                temp = temp.Remove(0, 10);
            temp = temp.Trim();
            if (temp.IndexOf("Lẻ") == 0)
                temp = temp.Remove(0, 2);
            temp = temp.Trim();
            temp = temp.Replace("Mươi Một", "Mươi Mốt");
            temp = temp.Trim();
            //Change Case
            var result = temp.Substring(0, 1).ToUpper() + temp.Substring(1).ToLower();
            return result;
        }
        //Create PDF
        [HttpPost]
        public Boolean CreatePdf(int invoiceID)
        {
              var stationId =  Convert.ToInt32(Session["StationID"]);
            var person = _db.Accounts.Where(p => p.StationID == stationId).FirstOrDefault();
            var get = _db.Invoices.Where(p => p.InvoiceID == invoiceID).FirstOrDefault();
            var textMoney = ReadMoney(get.Price.ToString());
            string htmlText1 = "<font face='arial unicode ms'>" +
                               "<div style='font: bold'>" +
                               "CÔNG TY XE KHÁCH I-DELIVER" +
                               "</div>" +
                               "----------------------------------------------------------------------------------------------------------------------------------" +
                               "<br/>" +
                               "<div style='font: bold; text-align: center; font-size: 20px'>" +
                               "HOÁ ĐƠN KHÁCH HÀNG" +
                               "</div>" +
                               "<br/>" +
                               "<div style='text-align: center;font-style: italic'>" +
                               "Ngày lập phiếu: " + DateTime.Now +
                               "</div>" +
                               "<br/>" +
                               "<div style='float: left'>" +
                               "Θ Tên khách hàng: " + get.Request.SenderName +
                               "<br/>" +
                               "Θ Địa chỉ người gửi: " + get.Request.SenderAddress +
                                  "<br/>" +
                               "Θ Địa chỉ người nhận: " + get.Request.ReceiverAddress +
                                  "<br/>" +
                               "Θ Mã hàng: " + get.Request.RequestCode +
                               "<br/>" +

                               "</div>" +
                               "<br/>" +
                               "<div style='font:bold;text-align: center;font-size: 13px'>" +
                               "THÔNG TIN HÀNG HOÁ" +
                               "<br/>" +
                               "≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈≈" +
                               "</div>" +

                               "<div style='text-align: center'>" +
                               "Khối lượng: " + get.Weight + " " +"kg" + "<p>-------------------------------------</p>" +
                               "Thể tích: " + get.Volume + " " + "dm3" + "<p>-------------------------------------</p>" +
                               "Thành tiền: " + get.Price + " " + "VND" + "<p>-------------------------------------</p>" +
                            
                               "</div>" +
                                "<div style='text-align: center;font-style: italic'>" +
                               "Tiền thành chữ: " + textMoney +
                               "</div>"+
                              
                               "<br/>" +
                               "<div style='text-align: left'>" +
                               "Người lập phiếu: " + person.UserInfo.Firstname + " " + person.UserInfo.Lastname +
                                "</div>" +
                                 "<div style='text-align: right'>" +
                                "Trạm lập phiếu: " + person.Station.StationName +
                                "</div>" + 
                               "</font>";
            HTMLToPdf(htmlText1, "PDFfile.pdf");
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
        public void HTMLToPdf(string HTML, string FilePath)
        {
            TextReader reader = new StringReader(HTML);
            string FONT = "c:/windows/fonts/arialuni.ttf";
            using (Document document = new Document(PageSize.LETTER))
            {
                string path = Server.MapPath("~/");
                PdfWriter.GetInstance(document, new FileStream(path + "/Invoice.pdf", FileMode.Create));
                HTMLWorker worker = new HTMLWorker(document);
                document.Open();
                FontFactory.Register(FONT, "arial unicode ms");
                // step 4.2: create a style sheet and set the encoding to Identity-H
                StyleSheet ST = new StyleSheet();
                ST.LoadTagStyle("body", "encoding", "Identity-H");

                // step 4.3: assign the style sheet to the html parser
                worker.SetStyleSheet(ST);

                worker.StartDocument();

                // step 5: parse the html into the document

                worker.Parse(reader);
                worker.EndDocument();
                worker.Close();
                document.Close();
            }
        }
        //
        public FileResult DisplayPDF()
        {
            string path = Server.MapPath("~/");
            return File(path + "\\Invoice.pdf", "application/pdf; charset=utf-8");
        }
        //
        public double CalculatePrice(float weight, float volume, int FromLocation, int ToLocation)
        {

            //    var list = _db.Stages.Where(p => p.StartPoint == FromLocation && p.EndPoint == ToLocation).Single();

            var getPrice = _db.ManageFees.ToList();
            double priceVolume = 0;
            for (var i = 0; i < getPrice.Count; i++)
            {
                if ((weight > getPrice[i].MinWeight && weight < getPrice[i].MaxWeight) && (volume > getPrice[i].MinVolume && volume < getPrice[i].MaxVolume))
                {
                    priceVolume = getPrice[i].Fee;
                }
                else if (weight > getPrice[i].MaxWeight && (volume > getPrice[i].MinVolume && volume < getPrice[i].MaxVolume))
                {
                    priceVolume = (weight / getPrice[i].MaxWeight) * getPrice[i].Fee;
                }
                else if (volume > getPrice[i].MaxVolume)
                {
                    priceVolume = (volume / getPrice[i].MaxVolume) * getPrice[i].Fee;
                }
            }
            return (priceVolume);
        }

        public void CancerRequest(int requestId)
        {
            var get = _db.Requests.Where(p => p.RequestID == requestId).FirstOrDefault();
            get.DeliveryStatusID = 7;
            _db.SaveChanges();

        }
    }
}