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
        public ActionResult ListProvince()
        {
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


        //public float LoadRateAvg(Int32 StationID)
        //{
        //    Station station = _db.Stations.Where(p => p.StationID == StationID).FirstOrDefault();
        //    return (float)station.AvgRatingLevel;
        //}

        public ActionResult LoagRatingAvg(int StationID)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Comment(String Username, String Comment, Int32 StationID)
        {
            Comment cmt = new Comment();
            cmt.Username = Username;
            cmt.CommentContent = Comment;
            cmt.DatePost = DateTime.Now;
            cmt.Status = false;
            cmt.StationID = StationID;
            _db.Comments.Add(cmt);
            _db.SaveChanges();
            return RedirectToAction("Index", "FeedBack");

        }

        [HttpPost]
        public ActionResult Rating(String Username, float Rating, Int32 StationID)
        {
            Rating rating = new Rating();
            Station station = new Station();
            
            rating = _db.Ratings.Where(r => r.StationID == StationID && r.Username == Username).FirstOrDefault();
            if (rating!= null)
            {
                rating.RateLevel = Rating;
                _db.Entry(rating).State = EntityState.Modified;
                _db.SaveChanges();
            }
            else
            {
                Rating rt = new Rating();
                rt.Username = Username;
                rt.RateLevel = Rating;
                rt.StationID = StationID;
                _db.Ratings.Add(rt);
                _db.SaveChanges();
            }


            float ratingAvg = RatingAvg(StationID);
            station = _db.Stations.Where(r => r.StationID == StationID).FirstOrDefault();
            if (station.AvgRatingLevel != null)
            {
                station.AvgRatingLevel = ratingAvg;
                _db.Entry(station).State = EntityState.Modified;
                _db.SaveChanges();
            }
            else {
                station.AvgRatingLevel = ratingAvg;
                _db.SaveChanges();
            }
            return View();
            //return RedirectToAction("Index", "FeedBack");
        }

        public float RatingAvg(Int32 StationID)
        {
            var ListRate = _db.Ratings.Where(p => p.StationID == StationID).ToList();
            float res = 0;
            foreach (var rate in ListRate)
            {
                res = res + (float)rate.RateLevel;
            }
            res = res / ListRate.Count;
            return res;
        }


    }
}
