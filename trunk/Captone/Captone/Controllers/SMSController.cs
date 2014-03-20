using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Captone.Models;
using System.Xml;

namespace eSMSAPI_Demo.Controllers
{
 
    public class SMSController : Controller
    {

        const string APIKey = "303C83DB697B80DA682FE234F8CE39";//Dang ky tai khoan tai esms.vn de lay key
        const string SecretKey = "AE6A14BA3637068A1AAADBC3A01900";
        //
        // GET: /SMS/
       iDeliverEntities _db = new iDeliverEntities();
        public void Send(List<Request> phone)
        {
            foreach(var item in phone)
            {
                var getLocation = _db.Stations.Where(p => p.StationID == item.ToLocation).Single();
            string message = "Công ty iDeliver xin thông báo. Món hàng bạn gửi đến " + getLocation.StationName+ " đã được chuyển đi !!!";
            var url = "http://api.esms.vn/MainService.svc/xml/SendMultipleSMS/"+item.SenderPhone+"/"+message+"/"+APIKey+"/"+SecretKey;
            var req = (HttpWebRequest)WebRequest.Create(url);
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
}
