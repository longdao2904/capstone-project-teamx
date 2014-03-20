using System.Web.Services;
using Captone.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
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

        public ActionResult LoadRequest()
        {
            var all = _db.Requests.ToList();
            if (Session["USERNAME"] != null)
            {
                string sender = Session["USERNAME"].ToString();
                if ((string)Session["UserRole"] == "Customer")
                {
                    pendingReq = _db.Requests.Where(r => r.Username == sender).Where(r => r.DeliveryStatusID == 1).ToList();
                    ViewBag.TranssitedReq = _db.Requests.Where(r => r.Username == sender).Where(r => r.DeliveryStatusID == 3).ToList();
                    ViewBag.ArrivedReq = _db.Requests.Where(r => r.Username == sender).Where(r => r.DeliveryStatusID == 4).ToList();
                    ViewBag.DeliveredReq = _db.Requests.Where(r => r.Username == sender).Where(r => r.DeliveryStatusID == 5).ToList();
                    return PartialView("LoadRequest", pendingReq);
                }
            }
            return PartialView("LoadRequest", all);
        }

        public ActionResult SentRequestForm()
        {

            if (Session["USERNAME"] == null)
            {
                return RedirectToAction("LogOn", "Account");
            }
            else
            {
                var manageFee = _db.ManageFees.ToList();
                return View(manageFee);
            }
        }

        public string CalculateFee(double minWeight, double maxWeight)
        {
            var fee = (from m
                           in _db.ManageFees
                       where minWeight == m.MinWeight &&
                             maxWeight == m.MaxWeight
                       select new { m.Fee }).Single();

            return fee.ToString();
        }

        public ActionResult GetAddressStation(string stationLocation)
        {

            // var address = (from m
            //        in _db.Stations
            //      where m.StationLocation.Contains(stationLocation)
            //       select new {m.StationName, m.StationLocation}
            //       ).ToList();
            //  return Json(address, JsonRequestBehavior.AllowGet);

            string str = "select * from dbo.Station where StationLocation like N'%" + stationLocation + "%'";
            var address = _db.Database.SqlQuery<Station>(str).ToList();
            return Json(address, JsonRequestBehavior.AllowGet);

        }

        public ActionResult PostRequest(FormCollection col)
        {
            Request request = new Request();
            request.Username = col["Username"];
            request.DeliveryStatusID = 1;
            request.FeeID = int.Parse(col["FeeID"]);
            request.EstimateWeight = col["EstimateWeight"];
            request.EstimateVolume = col["EstimateVolume"];
            request.DateRequest = DateTime.Parse(col["DateRequest"]);
            request.Description = "";
            request.FromLocation = int.Parse(col["FromLocation"]);
            request.ToLocation = int.Parse(col["ToLocation"]);
            request.SenderAddress = col["SenderAddress"];
            request.ReceiverAddress = col["ReceiverAddress"];
            request.SenderPhone = col["SenderPhone"];
            request.ReceiverPhone = col["ReceiverPhone"];
            request.ReceiverName = col["ReceiverName"];
            request.TypeOfPayment = col["TypeOfPayment"];
            _db.Requests.Add(request);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public List<Route> SearchRoute(string routes)
        {

            var list = new List<Route>();
            return list;
        }

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
            ViewBag.Province = province;
            return PartialView("ListProvince");

        }

        public ActionResult ListStatusCustomer(string Username)
        {
            IEnumerable<SelectListItem> request = (from p
                                                       in _db.Requests
                                                   where p.Username == Username
                                                   group p by new
                                                                  {
                                                                      p.DeliveryStatusID,
                                                                      p.DeliveryStatu.StatusName

                                                                  }
                                                       into k
                                                       select new SelectListItem()
                                                                  {
                                                                      Text = k.Key.StatusName,
                                                                      Value = SqlFunctions.StringConvert((double)k.Key.DeliveryStatusID).Trim()
                                                                  }
                                                  ).ToList();
            ViewBag.Status = request;
            return View();
        }

        public ActionResult ListStation(int province)
        {
            var station = _db.Stations.Where(p => p.ProvinceID == province).ToList();
            return PartialView("ListStation", station);
        }

        public ActionResult ListCoordinate(int ProvinceID)
        {
            var coordinate = (from p
                                  in _db.Stations
                              where p.ProvinceID == ProvinceID
                              select new { p.Coordinate }
                             ).ToList();
            return Json(coordinate, JsonRequestBehavior.AllowGet);
        }

        public List<Models.Request> pendingReq { get; set; }

        public ActionResult ListRequestCustomer(int DeliveryStatusID, string Username)
        {
            var list = _db.Requests.Where(p => p.DeliveryStatusID == DeliveryStatusID && p.Username == Username).ToList();
            return PartialView(list);

        }

        public Boolean DeleteRequest(int requestId)
        {
            var request = _db.Requests.Where(p => p.RequestID == requestId).Single();
            if (request != null)
            {
                _db.Requests.Remove(request);
                _db.SaveChanges();
                return true;
            }
            return false;
        
        }
  
        public ActionResult ListTripTracking(int requestId)
        {
            var tracking = _db.Assignings.Where(p => p.RequestID == requestId).ToList();
            return PartialView("ListTripTracking", tracking);
        }

        
    }
}

