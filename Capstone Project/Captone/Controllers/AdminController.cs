using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Captone.Models;

namespace Captone.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        private iDeliverEntities _db = new iDeliverEntities();

        public ActionResult PieChart()
        {
            var RouteID = _db.Routes.ToList();
            ArrayList array = new ArrayList();
            foreach (var item in RouteID)
            {
                var countRoute = _db.Trips.Count(p => p.RouteID == item.RouteID);
                var routeName = _db.Routes.Single(p => p.RouteID == item.RouteID);
                array.Add(countRoute + " : " + routeName.RouteName);
            }
            return Json(array, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Account model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_db.Accounts.Any(r => r.Username == model.Username && r.Password == model.Password))
                {
                    Session["USERNAME"] = model.Username;
                    var firstOrDefault = _db.Accounts.FirstOrDefault(u => u.Username == model.Username);
                    if (firstOrDefault != null)
                        Session["UserRole"] = firstOrDefault.Role;
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        return RedirectToAction("LogIn", "Admin");
                    if ((string) Session["UserRole"] == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return RedirectToAction("LogOn", "Account");
            }
            return RedirectToAction("LogIn", "Admin");
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session["USERNAME"] = null;
            Session["UserRole"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
