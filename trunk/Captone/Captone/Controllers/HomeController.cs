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
<<<<<<< .mine
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
=======
        public ActionResult SentRequestForm()
        {
            var ManageFee = _db.ManageFees.ToList();
            return View(ManageFee);
        }
        public ActionResult SuccessRegister()
        {
            return View();
        }
>>>>>>> .r97

<<<<<<< .mine
        public ActionResult AddRegister(FormCollection col)
        {
            Account account = new Account();
            account.Email = col["Email"];
            account.Username = col["Username"];
            account.Password = col["Password"];
            account.Phone = col["Phone"];
            account.Role = "Customer";
            Session["USERNAME"] = account.Username;
            _db.Accounts.Add(account);
            _db.SaveChanges();
            return RedirectToAction("SuccessRegister");
        }
        public Boolean CheckLogin(String Username, String Password)
        {
            var check = _db.Accounts.Where(p => p.Username == Username && p.Password == Password);
            if (check != null)
            {
                Session["USERNAME"] = Username;
                return true;
            }
=======
        public ActionResult AddRegister(FormCollection col)
        {
            Account account = new Account();
            account.Email = col["Email"];
            account.Username = col["Username"];
            account.Password = col["Password"];
            account.Phone = col["Phone"];
            account.Role = "Customer";
            Session["USERNAME"] = account.Username;
            _db.Accounts.Add(account);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public Boolean CheckLogin(String Username, String Password)
        {
            var check = _db.Accounts.Where(p => p.Username == Username && p.Password == Password);
            if (check != null)
            {
                Session["USERNAME"] = Username;
                return true;
            }
>>>>>>> .r97

            return false;

        }

<<<<<<< .mine
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
=======
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
        public void CalculateFee(float minWeight, float maxWeight, float minVolume, float maxVolume)
        {
>>>>>>> .r97

<<<<<<< .mine
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
                   select new {m.StationName, m.StationLocation}).ToList();
            return Json(address, JsonRequestBehavior.AllowGet);
        }
=======
            var fee = (from m
                           in _db.ManageFees
                       where minWeight == m.MinWeight &&
                             maxWeight == m.MaxWeight &&
                             minVolume == m.MinVolume &&
                             maxVolume == m.MaxVolume
                       select new { m.Fee });
            ViewData["FEE"] = fee;
        }
>>>>>>> .r97
    }

}
