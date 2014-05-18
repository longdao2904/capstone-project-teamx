using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Captone.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web.Mvc;
namespace Captone.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private iDeliverEntities _db = new iDeliverEntities();

        public ActionResult Index()
        {
            if (Session["USERNAME"] != null && Session["USERNAME"].ToString() == "guest") Session["USERNAME"] = null;
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
            if (Session["USERNAME"] != null && Session["USERNAME"].ToString() == "guest") Session["USERNAME"] = null;
            if (Session["USERNAME"] == null)
            {
                return RedirectToAction("SentRequestForm2");
            }
            else
            {
                var manageFee = _db.ManageFees.ToList();
                return View(manageFee);
            }
        }
        public ActionResult SentRequestForm2()
        {
            var manageFee = _db.ManageFees.ToList();
            var CoachType = _db.CoachTypes.ToList().Last();
            ViewBag.MaxLenght = CoachType.Length;
            ViewBag.MaxWidth = CoachType.Width;
            ViewBag.MaxHeight = CoachType.Height;
            return View(manageFee);
        }
        public string CalculateFee(float minVolume, float maxVolume)
        {
            var fee = (from m
                           in _db.ManageFees
                       where m.MinVolume == minVolume &
                             m.MaxVolume == maxVolume
                       select new { m.Fee, m.FeeID }).Single();

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
            var username = Session["USERNAME"];
            var fromlocation = int.Parse(col["FromLocation"]);
            var getStationID = _db.Stations.Where(p => p.StationID == fromlocation).Single();
            Notification notification = new Notification();

            notification.StationID = getStationID.StationID;
            notification.isView = false;
            _db.Notifications.Add(notification);
            Request request = new Request();
            if (username != null)
            {
                request.Username = username.ToString();
            }
            else
            {
                request.Username = "guest";
            }
            request.DeliveryStatusID = 1;
            request.FeeID = int.Parse(col["FeeID"]);
            request.EstimateWeight = col["EstimateWeight"];
            request.EstimateVolume = col["EstimateVolume"];
            request.DateRequest = DateTime.Parse(col["DateRequest"]);
            request.FromLocation = int.Parse(col["FromLocation"]);
            request.ToLocation = int.Parse(col["ToLocation"]);
            request.SenderName = col["Username"];
            request.SenderAddress = col["SenderAddress"];
            request.ReceiverAddress = col["ReceiverAddress"];
            request.SenderPhone = col["SenderPhone"];
            request.ReceiverName = col["ReceiverName"];
            request.ReceiverPhone = col["ReceiverPhone"];
            request.Type = true;
            request.TypeOfPayment = col["TypeOfPayment"];
            request.ArrivedDate = null;
            request.Payment = false;
            request.RequestCode = RandomString();
            request.Length = float.Parse(col["Length"]);
            request.Width = float.Parse(col["Width"]);
            request.Height = float.Parse(col["Height"]);
            _db.Requests.Add(request);
            _db.SaveChanges();
             Send();
            return RedirectToAction("Index");

        }
        private const string APIKey = "DE5C19CFB8888A5799AC714ADA7A6E"; //Dang ky tai khoan tai esms.vn de lay key
        private const string SecretKey = "D463CA2D47F215FBB49736577B976A";
        public void Send()
        {
            var request = _db.Requests.ToList().Last();
            var message = "Yeu cau van chuyen goi hang cua ban da duoc gui. Day la ma mon hang: " + request.RequestCode;
            string url = "http://api.esms.vn/MainService.svc/xml/SendMultipleMessage_V2/";
            // declare ascii encoding
            UTF8Encoding encoding = new UTF8Encoding();

            string strResult = string.Empty;

            string customers = "";

                customers = customers + @"<CUSTOMER>"
                                + "<PHONE>" + request.SenderPhone + "</PHONE>"
                                + "</CUSTOMER>";
            
            string SampleXml = @"<RQST>"
                               + "<APIKEY>" + APIKey + "</APIKEY>"
                               + "<SECRETKEY>" + SecretKey + "</SECRETKEY>"
                               + "<ISFLASH>0</ISFLASH>"
                               + "<UNICODE>1</UNICODE>"//=1 nếu muốn gửi có dấu, có dấu: 70 ký tự 1 tin, không dấu: 160 ký tự 1 tin
                               + "<CONTENT>" + message + "</CONTENT>"
                               + "<CONTACTS>" + customers + "</CONTACTS>"


           + "</RQST>";
            string postData = SampleXml.Trim().ToString();
            // convert xmlstring to byte using ascii encoding
            byte[] data = encoding.GetBytes(postData);
            // declare httpwebrequet wrt url defined above
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            // set method as post
            webrequest.Method = "POST";
            webrequest.Timeout = 500000;
            // set content type
            webrequest.ContentType = "application/x-www-form-urlencoded";
            // set content length
            webrequest.ContentLength = data.Length;
            // get stream data out of webrequest object
            Stream newStream = webrequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            // declare & read response from service
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();

            // set utf8 encoding
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            // read response stream from response object
            StreamReader loResponseStream =
                new StreamReader(webresponse.GetResponseStream(), enc);
            // read string from stream data
            strResult = loResponseStream.ReadToEnd();
            // close the stream object
            loResponseStream.Close();
            // close the response object
            webresponse.Close();
            // below steps remove unwanted data from response string
            strResult = strResult.Replace("</string>", "");

        }
        public JsonResult getUserInfo(string username)
        {
            var userInfo = _db.UserInfoes.Where(p => p.Username == username).FirstOrDefault();
            return Json(new {FirstName = userInfo.Firstname, Lastname = userInfo.Lastname, Address = userInfo.Address, PhoneNumber = userInfo.PhoneNumber}, JsonRequestBehavior.AllowGet);
        }
        public JsonResult checkLength(double length)
        {
            var coachType = _db.CoachTypes.ToList().Last();
            if (length > coachType.Length)
            {
                return Json(new { check = false }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { check = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult checkHeight(double height)
        {
            var coachType = _db.CoachTypes.ToList().Last();
            if (height > coachType.Height)
            {
                return Json(new { check = false }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { check = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult checkPackage(double width)
        {
            var coachType = _db.CoachTypes.ToList().Last();
 
            if (width > coachType.Width)
            {
                return Json(new { check = false }, JsonRequestBehavior.AllowGet);
            }
         
            return Json(new { check = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getVolume(float volume)
        {
            var list = _db.ManageFees.Single(p => p.MinVolume <= volume & p.MaxVolume >= volume);
            return Json(new {MinVolume = list.MinVolume, MaxVolume = list.MaxVolume}, JsonRequestBehavior.AllowGet);
        }
        protected string RandomString()
        {
            string allowedChars = "";
            allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "1,2,3,4,5,6,7,8,9,0";
            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string passwordString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < 7; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                passwordString += temp;
            }
            return passwordString;
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
                                                    where p.StationID != 11
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
            tracking.Sort((trip1, trip2) => (trip1.IndicateOrder).CompareTo(trip2.IndicateOrder));
            return PartialView("ListTripTracking", tracking);
        }

        public ActionResult GetRoutePrice(string RouteName)
        {
            string str = "select * from dbo.Stage where StageName like N'%" + RouteName + "%'";
            var address = _db.Database.SqlQuery<Stage>(str).ToList();
            return Json(address, JsonRequestBehavior.AllowGet);
        }

        public int getNotification(string username)
        {
            NotificationRepository obj = new NotificationRepository();
            //var getStaff = _db.Accounts.Where(p => p.Username == username).Single();
            //var getStation = _db.Notifications.Where(p => p.StationID == getStaff.StationID & p.isView == false).ToList();
            //var count = getStation.Count();
            //return count;
            var getdata = obj.GetData(username);
            return getdata;
        }

        public void DeleteNotification(int StationID)
        {

            var list = _db.Notifications.Where(p => p.isView == false & p.StationID == StationID).ToList();
            foreach (var notification in list)
            {
                _db.Notifications.Remove(notification);
                _db.SaveChanges();
            }
        }

        public ActionResult AcceptInvoice(int StationID)
        {
            var listInvoice = _db.Invoices.ToList();
            var list = new List<Invoice>();
            foreach (var invoice in listInvoice)
            {
                if (invoice.Request.FromLocation == StationID && invoice.Request.DeliveryStatusID == 1) list.Add(invoice);
            }
            return View(list);
        }
        //Details Station
        public ActionResult DetailsStation(int StationID)
        {
            var station = _db.Stations.Where(s => s.StationID == StationID).FirstOrDefault();
            ViewBag.DetailStation = station;
            return PartialView();
        }
        //Detail Route on Station
        public ActionResult DetailStationRoute(int ProvinceID)
        {
            var province = _db.Provinces.Where(p => p.ProvinceID == ProvinceID).FirstOrDefault();
            var provinceName = province.ProvinceName;
            if (provinceName == "Hồ Chí Minh")
            {
                provinceName = "Sài Gòn";
            }
            //string sql = "SELECT Distinct RouteName FROM dbo.Route WHERE RouteName LIKE N'" + provinceName + "%'";
            //var listRoute = _db.Database.SqlQuery<Route>(sql);

            IEnumerable<DetailStationModel> list = (from p
                              in _db.Stages
                                                    where p.StageName.StartsWith(provinceName)
                                                    select new DetailStationModel()
                                                    {
                                                        RouteName = p.StageName,
                                                        duration = p.Duration
                                                    }
                                 ).Distinct();

            return PartialView(list);
        }

        //Detail Request on Station
        public ActionResult DetailStationRequest(int StationID)
        {
            var request = _db.Requests.Where(r => r.FromLocation == StationID).ToList();
            return PartialView("DetailStationRequest", request);
        }
    }
}

