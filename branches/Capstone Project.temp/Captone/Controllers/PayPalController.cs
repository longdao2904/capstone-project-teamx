using Captone.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Captone.Models;

namespace Captone.Controllers
{
    public class PayPalController : Controller
    {
        //[Authorize(Roles="Customers")]
        public ActionResult ValidateCommand(string item_name, string amount)
        {
            bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSandbox"]);
            var paypal = new PayPalModel(useSandbox);
            paypal.amount = amount;
            paypal.item_name = item_name;
            return View(paypal);
        }

        public ActionResult RedirectFromPaypal()
        {
            return View();
        }

        public ActionResult CancelFromPaypal()
        {
            return View();
        }

        public ActionResult NotifyFromPaypal()
        {
            return View();
        }
    }
}
