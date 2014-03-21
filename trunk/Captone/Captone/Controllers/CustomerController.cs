using System.Net;
using System.Web.Services;
using Captone.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
namespace Captone.Controllers
{
    public class CustomerController : Controller
    {
        iDeliverEntities _db = new iDeliverEntities();
        public ActionResult Index()
        {
            var username = Session["USERNAME"];
            var list = _db.Requests.Where(p => p.DeliveryStatusID == 1 & p.TypeOfPayment == "Thanh toán trực tuyến" & p.Payment == false & p.Username == (string) username).ToList();
            return View(list);
        }
        public ActionResult InputPayment(int RequestID)
        {
            var single = _db.Invoices.Where(p => p.RequestID == RequestID).Single();
            return View(single);
        }
        public ActionResult ReviewInfor(int requestID)
        {
            var request = new Cookie("requestId", requestID.ToString());
            var single = _db.Invoices.SingleOrDefault(p => p.RequestID == requestID);
            var req = _db.Requests.SingleOrDefault(p => p.RequestID == requestID);

            var ret = new InvoiceModel
                {
                    Invoice = single,
                    Request = req
                };
            return View(ret);
        }
    }
}
