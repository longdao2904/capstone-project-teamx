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
            if (Session["USERNAME"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            else
            {
                return View(_db.Stations.ToList());
            }
        }
        //List Province
        public ActionResult ListProvince()
        {
            if (Session["USERNAME"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            else
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
        }
        //List Station of Province
        public ActionResult ListStation(int province)
        {
            if (Session["USERNAME"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            else
            {
                var station = _db.Stations.Where(p => p.ProvinceID == province).ToList();
                return PartialView("ListStation", station);
            }
        }
        //List Comment of Station
        public ActionResult ListComment(int station)
        {
            if (Session["USERNAME"] == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            else
            {
                var comment = _db.Comments.Where(p => p.StationID == station).ToList();
                ViewBag.StationSelect = _db.Stations.Where(s => s.StationID == station).FirstOrDefault();
                return PartialView("ListComment", comment);
            }
        }

        //List All Comment to Manage
        public ActionResult ManageCommentList()
        {
            return View(_db.Comments.ToList());
        }
        //List All Rating to Manage
        public ActionResult ManageRatingList() 
        {
            return View(_db.Stations.ToList());
        }
        //ClearRating
        public ActionResult ClearRating(int id)
        {
            Station station = _db.Stations.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }
        //Clear Rating Confirm
        [HttpPost, ActionName("ClearRating")]
        public ActionResult ClearRatingConfirmed(int id)
        {
            Station station = _db.Stations.Where(s => s.StationID == id).FirstOrDefault();
            List<Rating> rating = _db.Ratings.Where(s => s.StationID == id).ToList();
            foreach (var item in rating)
            {
                _db.Ratings.Remove(item);
                _db.SaveChanges();
            }
            station.AvgRatingLevel = 0;
            _db.Entry(station).State = EntityState.Modified;
         
            _db.SaveChanges();
            return RedirectToAction("ManageRatingList");
        }

        //Delete Comment
        public ActionResult DeleteComment(int id)
        {
            Comment comment = _db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }
        [HttpPost, ActionName("DeleteComment")]
        public ActionResult DeleteCommentConfirmed(int id)
        {
            Comment cmt = _db.Comments.Where(c => c.CommentID == id).FirstOrDefault();
            _db.Comments.Remove(cmt);
            _db.SaveChanges();
            return RedirectToAction("ManageCommentList");
        }

        //Comment
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
        //Rating
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
            //Check AvgRating, if Exist then override
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
        //Calculate RatingAvg
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
