using Captone.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Captone.Services.Interfaces;

namespace Captone.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IAccountService _accountService;
        public CustomerController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public ActionResult Index()
        {
            return View(_accountService.GetAllCustomers());
        }

        public ActionResult Create(string username)
        {
            return View();
        }

    }
}
