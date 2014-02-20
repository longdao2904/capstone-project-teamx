using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Captone.Models;
using System.IO;
namespace Captone.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private iDeliverEntities _db = new iDeliverEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }



        public ActionResult SentRequestForm()
        {

            if (Session["USERNAME"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var ManageFee = _db.ManageFees.ToList();
                return View(ManageFee);
            }
        }
        public ActionResult SuccessRegister()
        {
            return View();
        }

        public Boolean CheckLogin(String Username, String Password)
        {
            var check = _db.Accounts.Where(p => p.Username == Username && p.Password == Password);
            if (check != null)
            {
                Session["USERNAME"] = Username;
                return true;
            }

            return false;
        }
 
        public ActionResult AddUserInfo(FormCollection col)
        {
            UserInfo info = new UserInfo();
            info.Username = Session["USERNAME"].ToString();
            info.Firstname = col["Firstname"];
            info.Lastname = col["Lastname"];
            info.Address = col["Address"];
            _db.UserInfoes.Add(info);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public string CalculateFee(double minWeight, double maxWeight)
        {
            var fee = (from m
                           in _db.ManageFees
                       where minWeight == m.MinWeight &&
                             maxWeight == m.MaxWeight
                       select new { m.Fee }).Single();

            return fee.ToString();
        }

        public ActionResult GetAddressStation()
        {
            var address = (from m
                       in _db.Stations
                           select new { m.StationName, m.StationLocation }).ToList();
            return Json(address, JsonRequestBehavior.AllowGet);
        }
    }
}
