using System;
using System.Linq;
using System.Web.Mvc;
using Captone.Models;
using System.Web.Security;

namespace Captone.Controllers
{
    public class AccountController : Controller
    {
        private iDeliverEntities _db = new iDeliverEntities();

        #region For popup modal
        public ActionResult LogOn()
        {
            return View();
        }
        public bool CheckLogin(string username, string password, string returnUrl)
        {
            
            var check = _db.Accounts.Where(p => p.Username == username & p.Password == password).FirstOrDefault();
            if(check != null)
            {
                Session.Timeout = 60;
                Session["USERNAME"] = check.Username;
                Session["StationID"] = check.StationID;
                if (check.StationID != 0)
                {
                    var getStation = _db.Stations.Where(p => p.StationID == check.StationID).Single();
                    Session["StationName"] = getStation.StationName;
                }
                Session["UserRole"] = check.Role;
                FormsAuthentication.SetAuthCookie(check.Username, false);

                return true;

            }

            // If we got this far, something failed, redisplay form
            return false;
        }
        public ActionResult RequestCode(string requestCode)
        {
            var request = _db.Requests.Where(p => p.RequestCode == requestCode).FirstOrDefault();
            return View(request);
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session["USERNAME"] = null;
            Session["UserRole"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
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
                }
                Session["USERNAME"] = tmp.Username;
                return RedirectToAction("SuccessRegister", "Account");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

       

        public ActionResult RegAcc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegAcc(Account model)
        {
            if (ModelState.IsValid)
            {
                Account tmp = new Account();
                tmp.Username = model.Username;
                tmp.Password = model.Password;
                tmp.Email = model.Email;
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
                }
                Session["USERNAME"] = tmp.Username;
                return RedirectToAction("SuccessRegister", "Account");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
       

        public ActionResult SuccessRegister()
        {
            return View();
        }

        //Update user information after register
        public ActionResult AddUserInfo(FormCollection col)
        {
            UserInfo info = new UserInfo();
            info.Username = Session["USERNAME"].ToString();
            info.Firstname = col["Firstname"];
            info.Lastname = col["Lastname"];
            info.Address = col["Address"];
            _db.UserInfoes.Add(info);
            _db.SaveChanges();
            return RedirectToAction("SentRequestForm", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }


    }
}