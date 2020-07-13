using APPCOVID.BAL.Helpers.Account;
using APPCOVID.Controllers.Core;
using APPCOVID.Entity.ViewModels;
using APPCOVID.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace APPCOVID.Controllers
{
    public class HomeController : CovidController
    {
        private readonly ILogger<HomeController> _logger;
        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }

        public IActionResult Index()
        {
            Authorize();
            
            return View();
        }

        public IActionResult Privacy()
        {
            Authorize();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ViewUser() {
            Authorize("admin");
            ViewBag.IsAdminUser = IsUserInAdminRole;
            return View("~/Views/Home/ViewUser.cshtml");
        }

        [HttpPost]
        public IActionResult RegisterCustomer(RegisterInfoViewModel register)
        {
            Authorize("admin");
            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.UserAccount = new UserAccountModel
            {
                LASTLOGINDATE = DateTime.Now.ToString("ddMMyyyyhhmmtt"),               
                USERNAME = register.Email,
                ACTIVATEAC=1,
                PASSWORD = "Cu$t0mer"
            };
            registerViewModel.UserInfo = new UserInformationViewModel { NAME = register.FullName, EMAIL = register.Email, PHONENO = register.Phone };
            registerViewModel.UserType = "customer";
            _accontHelper.Register(registerViewModel);
            return RedirectToActionPermanent("Index", "Home");
        }
    }
}
