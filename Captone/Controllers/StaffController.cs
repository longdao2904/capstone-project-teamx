using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Captone.Models;
using Captone.Repositories;

namespace Captone.Controllers
{
    public class StaffController : Controller
    {
        private iDeliverEntities db = new iDeliverEntities();
        private GenericRepository<StaffModel> _staff = new GenericRepository<StaffModel>(new iDeliverEntities());
        public List<StaffModel> Staffs = new List<StaffModel>();
        public void InitData()
        {
            var accounts = db.Accounts.ToList();
            foreach (var account in accounts)
            {
                if (account.Role == "Staff" && account.StationID > 0)
                {
                    var staff = new StaffModel();
                    staff.Username = account.Username;
                    staff.Firstname = account.UserInfo.Firstname;
                    staff.Lastname = account.UserInfo.Lastname;
                    staff.PhoneNumber = account.UserInfo.PhoneNumber;
                    staff.Password = account.Password;
                    staff.BackupPassword = account.BackupPassword;
                    staff.Email = account.Email;
                    staff.Address = account.UserInfo.Address;
                    staff.StationID = account.StationID;
                    Staffs.Add(staff);
                }
            }
        }
        public ActionResult Index()
        {
            InitData();
            return View(Staffs);
        }

        //
        // GET: /Coach/Details/5

        public ActionResult Details(string username)
        {
            var staff = Find(username);
            return View(staff);
        }

        //
        // GET: /Coach/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Coach/Create

        [HttpPost]
        public ActionResult Create(StaffModel staff)
        {
            if (ModelState.IsValid)
            {
                //add account to db
                var account = new Account();
                account.Username = staff.Username;
                account.Password = staff.Password;
                account.BackupPassword = staff.BackupPassword;
                account.BannedStatus = false;
                account.StationID = staff.StationID;
                account.Role = "Staff";
                db.Accounts.Add(account);
                //add userinfor to db
                var userinfo = new UserInfo();
                userinfo.Username = staff.Username;
                userinfo.Address = staff.Address;
                userinfo.Firstname = staff.Firstname;
                userinfo.Lastname = staff.Lastname;
                userinfo.PhoneNumber = staff.PhoneNumber;
                db.UserInfoes.Add(userinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        //
        // GET: /Coach/Edit/5

        public ActionResult Edit(string username)
        {
            var staff = Find(username);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        //
        // POST: /Coach/Edit/5

        [HttpPost]
        public ActionResult Edit(StaffModel staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        public StaffModel Find(string username)
        {
            InitData();
            var staff = new StaffModel();
            foreach (var staffModel in Staffs)
            {
                if (staffModel.Username == username) staff = staffModel;
            }
            return staff;
        }
        //
        // GET: /Coach/Delete/5

        public ActionResult Delete(string username)
        {
            var staff = Find(username);
            return View(staff);
        }

        //
        // POST: /Coach/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string username)
        {
            var account = db.Accounts.Find(username);
            var userinfor = db.UserInfoes.Find(username);
            db.Accounts.Remove(account);
            db.UserInfoes.Remove(userinfor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}