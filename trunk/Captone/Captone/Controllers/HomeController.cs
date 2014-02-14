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
            var ManageFee = _db.ManageFees.ToList();
            return View(ManageFee);
        }
        public ActionResult SuccessRegister()
        {
            return View();
        }

        public ActionResult AddRegister(FormCollection col)
        {
            Account account = new Account();
            account.Email = col["Email"];
            account.Username = col["Username"];
            account.Password = col["Password"];
            account.Phone = col["Phone"];
            account.IsAdmin = false;
            account.RoleID = 3;
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
        public void CalculateFee(float minWeight, float maxWeight, float minVolume, float maxVolume)
        {

            var fee = (from m
                           in _db.ManageFees
                       where minWeight == m.MinWeight &&
                             maxWeight == m.MaxWeight &&
                             minVolume == m.MinVolume &&
                             maxVolume == m.MaxVolume
                       select new {m.Fee});
            ViewData["FEE"] = fee;
        }
    }
}
