using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Data.EntityClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Captone.Models;
using System.IO;
using System.Web.Security;
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

        [HttpPost]
        public ActionResult Register(Account model)
        {
            if (ModelState.IsValid)
            {
                Account tmp = new Account();
                tmp.Username = model.Username;
                tmp.Password = model.Password;
                tmp.Email = model.Email;
                tmp.Phone = model.Phone;
                tmp.Role = "Customer";
                tmp.BannedStatus = false;
                // Attempt to register the user
                _db.Accounts.Add(tmp);
                try
                {
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Register error");
                    //ViewBag.Message = e.StackTrace;
                    //return PartialView("Error");
                }
                Session["USERNAME"] = tmp.Username;
                return RedirectToAction("Index", "Home");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session["USERNAME"] = null;
            Session["UserRole"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SentRequestForm()
        {

            if (Session["USERNAME"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var manageFee = _db.ManageFees.ToList();
                return View(manageFee);
            }
        }

        public ActionResult SuccessRegister()
        {
            return View();
        }

        public Boolean CheckLogin(String Username, String Password)
        {
            var check = _db.Accounts.Where(p => p.Username == Username && p.Password == Password).Single();
            if (check != null)
            {
                Session["USERNAME"] = Username;
                Session["UserRole"] = check.Role;
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

        #region Request
        public string CalculateFee(double minWeight, double maxWeight)
        {
            var fee = (from m
                           in _db.ManageFees
                       where minWeight == m.MinWeight &&
                             maxWeight == m.MaxWeight
                       select new { m.Fee }).Single();

            return fee.ToString();
        }

        public ActionResult GetAddressStation(string stationLocation)
        {
    
           // var address = (from m
                       //        in _db.Stations
                     //      where m.StationLocation.Contains(stationLocation)
                    //       select new {m.StationName, m.StationLocation}
                   //       ).ToList();
          //  return Json(address, JsonRequestBehavior.AllowGet);

            string str = "select * from dbo.Station where StationLocation like N'%" + stationLocation + "%'";
           var address = _db.Database.SqlQuery<Station>(str).ToList();
            return Json(address, JsonRequestBehavior.AllowGet);

        }

        public ActionResult PostRequest(FormCollection col)
        {

            return View();
        }

        public List<Route> SearchRoute(string routes)
        {

            var list = new List<Route>();
            return list;
        }
        #endregion
    }
}
