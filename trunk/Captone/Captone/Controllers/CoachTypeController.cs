using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Captone.Models;
using System.Web.Services;

namespace Captone.Controllers
{
    public class CoachTypeController : Controller
    {
        private iDeliverEntities db = new iDeliverEntities();

        #region CRUD
        //
        // GET: /CoachType/

        public ActionResult Index()
        {
            return View(db.CoachTypes.ToList());
        }

        //
        // GET: /CoachType/Details/5

        public ActionResult Details(int id = 0)
        {
            CoachType coachtype = db.CoachTypes.Find(id);
            if (coachtype == null)
            {
                return HttpNotFound();
            }
            return View(coachtype);
        }

        //
        // GET: /CoachType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CoachType/Create

        [HttpPost]
        public ActionResult Create(CoachType coachtype)
        {
            if (ModelState.IsValid)
            {
                db.CoachTypes.Add(coachtype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coachtype);
        }

        //
        // GET: /CoachType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CoachType coachtype = db.CoachTypes.Find(id);
            if (coachtype == null)
            {
                return HttpNotFound();
            }
            return View(coachtype);
        }

        //
        // POST: /CoachType/Edit/5

        [HttpPost]
        public ActionResult Edit(CoachType coachtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coachtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coachtype);
        }

        //
        // GET: /CoachType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CoachType coachtype = db.CoachTypes.Find(id);
            if (coachtype == null)
            {
                return HttpNotFound();
            }
            return View(coachtype);
        }

        //
        // POST: /CoachType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CoachType coachtype = db.CoachTypes.Find(id);
            db.CoachTypes.Remove(coachtype);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [WebMethod]
        public ActionResult CoachTypePartial()
        {
            return View(db.CoachTypes.ToList());
        }
    }
}