using Captone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Captone.Controllers
{
    public class InvoiceController : Controller
    {
        private iDeliverEntities _db = new iDeliverEntities();
        //
        // GET: /Invoice/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RequestInfo(int requestID)
        {
            ViewBag.RequestInfo = _db.Requests.Where(r => r.RequestID == requestID).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult CreateInvoice(float Weight, float Volume, float Price, int RequestID)
        {
            try
            {
                Invoice invoice = new Invoice();
                invoice.Weight = Weight;
                invoice.Volume = Volume;
                invoice.Price = Price;
                invoice.RequestID = RequestID;
                _db.Invoices.Add(invoice);
                _db.SaveChanges();
                return RedirectToAction("Index", "Invoice");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Invoice/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Invoice/Edit/5

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
        // GET: /Invoice/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Invoice/Delete/5

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
