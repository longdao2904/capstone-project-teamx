using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Captone.Models;
using System.Data.Objects.SqlClient;


namespace Captone.Content
{
    public class FeedBackController : Controller
    {
        //
        // GET: /FeedBack/

        private iDeliverEntities _db = new iDeliverEntities();

        public ActionResult Index()
        {
            return View(_db.Stations.ToList());
        }
        /// <summary>
        /// list province
        /// </summary>
        /// <returns></returns>
        public ActionResult ListProvince() {
            IEnumerable<SelectListItem> province = (from p
                           in _db.Stations
                                                    group p by new
                                                    {
                                                        p.ProvinceID,
                                                        p.Province.ProvinceName
                                                    } into k
                                                    select new SelectListItem()
                                                    {
                                                        Text = k.Key.ProvinceName,
                                                        Value = SqlFunctions.StringConvert((double)k.Key.ProvinceID).Trim()
                                                    }).ToList();
            ViewBag.ListProvince = province;
            return PartialView("ListProvince");
        }

        public ActionResult ListStation(int province)
        {
            var station = _db.Stations.Where(p => p.ProvinceID == province).ToList();
            return PartialView("ListStation", station);
        }

        public ActionResult ListComment(int station)
        {
            var comment = _db.Comments.Where(p => p.StationID == station).ToList();
            ViewBag.StationSelect = _db.Stations.Where(s => s.StationID == station).FirstOrDefault();
            return PartialView("ListComment", comment);
        }

        public ActionResult Feedback() {
            return View();
        }
        //
        // GET: /FeedBack/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FeedBack/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /FeedBack/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FeedBack/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FeedBack/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FeedBack/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FeedBack/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
