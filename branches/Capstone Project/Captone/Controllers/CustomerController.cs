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
            var list = _db.Requests.Where(p => p.TypeOfPayment == "Chuyển khoản" & p.Payment == false & p.Username == username).ToList();
            return View(list);
        }
        public ActionResult InputPayment(int requestID)
        {
            var cookie = new HttpCookie("requestId");
            cookie.Value = requestID.ToString();
            Response.Cookies.Add(cookie);
            var single = _db.Invoices.SingleOrDefault(p => p.RequestID == requestID);
            if (single != null) single.Price = Math.Ceiling(single.Price/20000);
            var req = _db.Requests.SingleOrDefault(p => p.RequestID == requestID);
            var ret = new InvoiceModel
            {
                Invoice = single,
                Request = req
            };
            return View(ret);
        }
        //[HttpGet]
        //public ActionResult ReviewInfor(int requestID)
        //{
        //    var cookie = new HttpCookie("requestId");
        //    cookie.Value = requestID.ToString();
        //    Response.Cookies.Add(cookie);
        //    var single = _db.Invoices.SingleOrDefault(p => p.RequestID == requestID);
        //    var req = _db.Requests.SingleOrDefault(p => p.RequestID == requestID);
        //    var ret = new InvoiceModel
        //        {
        //            Invoice = single,
        //            Request = req
        //        };
        //    return View(ret);
        //}
        public ActionResult TablePrice()
        {
            var list = _db.ManageFees.ToList();
            return View(list);
        }
        public ActionResult TablePrice2()
        {
            var list = _db.Stages.ToList();
            return PartialView(list);
        }
    }
}
