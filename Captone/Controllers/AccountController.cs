using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Captone.Models;
using System.Web.Security;

namespace Captone.Controllers
{
    public class AccountController : Controller
    {
        private iDeliverEntities _db = new iDeliverEntities();
        public int SpecialStationID = 11;
        #region For popup modal
        public ActionResult LogOn()
        {
            return View();
        }
        public bool CheckLogin(string username, string password, string returnUrl)
        {
            MD5 md5Hash = MD5.Create();
            var users = _db.Accounts.ToList();
            Account check = null;
            foreach (var account in users)
            {
                if(account.Username == username && VerifyMd5Hash(md5Hash, password, account.Password)) check = account;
            }
       
            if(check != null)
            {
                Session.Timeout = 60;
                Session["USERNAME"] = check.Username;
                Session["StationID"] = check.StationID;
                if (check.StationID != 0)
                {
                    var getStation = _db.Stations.Single(p => p.StationID == check.StationID);
                    Session["StationName"] = getStation.StationName;
                }
                Session["UserRole"] = check.Role;
                FormsAuthentication.SetAuthCookie(check.Username, false);

                return true;

            }

            // If we got this far, something failed, redisplay form
            return false;
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

        public void Modify()
        {
            MD5 md5Hash = MD5.Create();
            var accounts = _db.Accounts.ToList();
            foreach (var account in accounts)
            {
                string hash = GetMd5Hash(md5Hash, account.Password);
                account.Password = hash;
            }
            _db.SaveChanges();
        }

        [HttpPost]
        public ActionResult Register(Account model)
        {
            MD5 md5Hash = MD5.Create();
            if (ModelState.IsValid)
            {
                Modify();
                var tmp = new Account();
                tmp.Username = model.Username;
                string hash = GetMd5Hash(md5Hash, model.Password);
                tmp.Password = hash;
                tmp.BackupPassword = hash;
                tmp.Email = model.Email;
                tmp.Role = "Customer";
                tmp.StationID = SpecialStationID;
                tmp.BannedStatus = false;
                // Attempt to register the user
                _db.Accounts.Add(tmp);
                try
                {
                    _db.SaveChanges();
                    Session["USERNAME"] = tmp.Username;
                    return RedirectToAction("SuccessRegister", "Account");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", @"Register error");
                }
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
            info.PhoneNumber = col["PhoneNumber"];
            _db.UserInfoes.Add(info);
            _db.SaveChanges();
            return RedirectToAction("SentRequestForm", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

    static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }

        // Verify a hash against a string. 
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input. 
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            var comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}