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
            return View();
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
            _db.Accounts.Add(account);
            _db.SaveChanges();
            return RedirectToAction("SuccessRegister");
        }
        
    }
}
