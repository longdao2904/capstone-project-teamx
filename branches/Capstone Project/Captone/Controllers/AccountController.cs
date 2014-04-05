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

        [HttpPost]
        public ActionResult LogOn(Account model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (model.Username == null | model.Password == null)
                {
                    ModelState.AddModelError("LoginFailed", "Tên đăng nhập hoặc mật khẩu không đúng.");
                    return RedirectToAction("LogOn", "Account");
                }
                else
                {
                    var check = _db.Accounts.Where(r => r.Username == model.Username && r.Password == model.Password).SingleOrDefault();
                    if (check != null)
                    {
                        Session.Timeout = 60;
                        Session["USERNAME"] = model.Username;
                        Session["StationID"] = check.StationID;
                        if (check.StationID != 0)
                        {
                            var getStation = _db.Stations.Where(p => p.StationID == check.StationID).Single();
                            Session["StationName"] = getStation.StationName;
                        }
                        Session["UserRole"] = _db.Accounts.Where(u => u.Username == model.Username).FirstOrDefault().Role;
                        FormsAuthentication.SetAuthCookie(model.Username, false);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return RedirectToAction("LogOn", "Home");
                        }
                        else
                        {
                            if ((string)Session["UserRole"] == "Customer")
                            {
                                return RedirectToAction("SentRequestForm", "Home");
                            }
                            else if ((string)Session["UserRole"] == "Staff")
                            {
                                return RedirectToAction("Index", "Request");
                            }
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        TempData["ERROR"] = "Tên đăng nhập hoặc mật khẩu không đúng. Vui lòng thử lại";
                        ModelState.AddModelError("LoginFailed", "Tên đăng nhập hoặc mật khẩu không đúng.");
                        return RedirectToAction("LogOn", "Account");
                    }
                }
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
                }
                Session["USERNAME"] = tmp.Username;
                return RedirectToAction("SuccessRegister", "Account");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

        #region For session catching
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Account model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (model.Username == null | model.Password == null)
                {
                    ModelState.AddModelError("LoginFailed", "Tên đăng nhập hoặc mật khẩu không đúng.");
                    return RedirectToAction("LogOn", "Account");
                }
                else
                {
                    var check = _db.Accounts.Where(r => r.Username == model.Username && r.Password == model.Password).Single();
                    if (check != null)
                    {
                        Session.Timeout = 60;
                        Session["USERNAME"] = model.Username;
                        Session["StationID"] = check.StationID;
                        if (check.StationID != 0)
                        {
                            var getStation = _db.Stations.Where(p => p.StationID == check.StationID).Single();
                            Session["StationName"] = getStation.StationName;
                        }
                        Session["UserRole"] = _db.Accounts.Where(u => u.Username == model.Username).FirstOrDefault().Role;
                        FormsAuthentication.SetAuthCookie(model.Username, false);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return RedirectToAction("LogOn", "Home");
                        }
                        else
                        {
                            if ((string)Session["UserRole"] == "Customer")
                            {
                                return RedirectToAction("SentRequestForm", "Home");
                            }
                            else if ((string)Session["UserRole"] == "Staff")
                            {
                                return RedirectToAction("Index", "Request");
                            }
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        TempData["ERROR"] = "Tên đăng nhập hoặc mật khẩu không đúng. Vui lòng thử lại";
                        ModelState.AddModelError("LoginFailed", "Tên đăng nhập hoặc mật khẩu không đúng.");
                        return RedirectToAction("LogOn", "Account");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

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
                }
                Session["USERNAME"] = tmp.Username;
                return RedirectToAction("SuccessRegister", "Account");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion

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