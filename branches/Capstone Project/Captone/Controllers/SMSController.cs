using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Captone.Models;
using System.Xml;

namespace Captone.Controllers
{

    public class SMSController : Controller
    {
        private const string APIKey = "9AFF0A9D07A3B94E2F6409A45D7194"; //Dang ky tai khoan tai esms.vn de lay key
        private const string SecretKey = "6DB23219CD5A1D7F61013989AD764F";
        //
        // GET: /SMS/
        private iDeliverEntities _db = new iDeliverEntities();

        public void Send(List<Request> phone)
        {
            foreach (var item in phone)
            {
                var getLocation = _db.Stations.Where(p => p.StationID == item.ToLocation).Single();
                string message = "Công ty iDeliver xin thông báo. Món hàng bạn gửi đến " + getLocation.StationName +
                                 " đã được chuyển đi !!!";
                var url = "http://api.esms.vn/MainService.svc/xml/SendMultipleSMS/" + item.SenderPhone + "/" + message +
                          "/" + APIKey + "/" + SecretKey;
                var req = (HttpWebRequest) WebRequest.Create(url);
                var res = req.GetResponse();

                XmlTextReader reader = new XmlTextReader(url);

                // Skip non-significant whitespace  
                reader.WhitespaceHandling = WhitespaceHandling.Significant;

                // Read nodes one at a time  
                //while (reader.Read())
                //{                
                //    System.Diagnostics.Debug.WriteLine("{0}: {1} - {2}", reader.NodeType.ToString(), reader.Name,reader.Value);                
                //}
                using (var reader1 = new System.IO.StreamReader(res.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    string responseText = reader1.ReadToEnd();
                }
            }

        }

        public void Send()
        {

            var SenderPhone = 0906425539;
            string message = "Công ty iDeliver xin thông báo. Món hàng bạn gửi đi đã được chuyển đi !!!";
            var url = "http://api.esms.vn/MainService.svc/xml/SendMultipleSMS/" + SenderPhone + "/" + message + "/" +
                      APIKey + "/" + SecretKey;
            var req = (HttpWebRequest) WebRequest.Create(url);
            var res = req.GetResponse();

            XmlTextReader reader = new XmlTextReader(url);

            // Skip non-significant whitespace  
            reader.WhitespaceHandling = WhitespaceHandling.Significant;

            // Read nodes one at a time  
            //while (reader.Read())
            //{                
            //    System.Diagnostics.Debug.WriteLine("{0}: {1} - {2}", reader.NodeType.ToString(), reader.Name,reader.Value);                
            //}
            using (var reader1 = new System.IO.StreamReader(res.GetResponseStream(), System.Text.Encoding.UTF8))
            {
                string responseText = reader1.ReadToEnd();
            }


        }
    }
}
