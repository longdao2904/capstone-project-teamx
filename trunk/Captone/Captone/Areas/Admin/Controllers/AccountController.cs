using Captone.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Captone.Services.Interfaces;

namespace Captone.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public ActionResult Index()
        {
            return View(_accountService.GetAllAccounts());
        }

        public ActionResult Edit(string username)
        {
            return View();
        }

    }
}
