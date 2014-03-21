using System.Web;
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
            var list = _db.Requests.Where(p => p.TypeOfPayment == "Thanh toán online" & p.Payment == false & p.Username == username).ToList();
            return View(list);
        }
        public ActionResult InputPayment(int RequestID)
        {
            var single = _db.Invoices.Where(p => p.RequestID == RequestID).Single();
            return View(single);
        }
        [HttpGet]
        public ActionResult ReviewInfor(int requestID)
        {
            var cookie = new HttpCookie("requestId");
            cookie.Value = requestID.ToString();
            Response.Cookies.Add(cookie);
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
