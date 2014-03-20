using System.Web.Mvc;
using Captone.Models;
using Captone.Services.Interfaces;

namespace Captone.Areas.Admin.Controllers
{
    public class StaffController : Controller
    {
        private readonly IAccountService _staffService;

        public StaffController(IAccountService staffService)
        {
            _staffService = staffService;
        }

        public ActionResult Index()
        {
            var model = _staffService.GetAllStaff();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(string username)
        {
            var model = _staffService.GetAccount(username);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Account account)
        {
            _staffService.EditAccount(account, Resource.Admin);
            return View();
        }

        public ActionResult Create()
        {
            var model = _staffService.GetAllStaff();
            return View(model);
        }
    }
}