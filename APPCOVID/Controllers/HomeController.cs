using APPCOVID.BAL;
using APPCOVID.BAL.Helpers;
using APPCOVID.BAL.Helpers.Account;
using APPCOVID.Controllers.Core;
using APPCOVID.Entity.ViewModels;
using APPCOVID.Models;
using APPCOVID.Models.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
            if (ViewBag.roleId == 3)
            {
                IList<InfectionSeverityCodesModel> infections = new ConversationHelper().FindInfections(this.CurrentUserId, "OST");
                int result = 0;
                int affectedStage = 0;
                result = infections.Where(t => t.Answer).Sum(t => t.SeverityCode);
                HttpContext.Session.SetObject("OST_Result", result.ToString());
                ViewBag.testResultDetails = result > 0 ? infections : null;
                if (result > 15 && result <= 30)
                {
                    affectedStage = 3;
                }
                if (result > 5 && result <= 15)
                {
                    affectedStage = 2;
                }
                if (result <= 5)
                {
                    affectedStage = 1;
                }
                HttpContext.Session.SetObject("OST_Result_Stage", affectedStage.ToString());
                ViewBag.affectedStage = affectedStage;
            }
            ViewBag.UserId = this.CurrentUserId;
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
                LASTLOGINDATE = DateTime.Now.ToString("dd-MM-yyyy hh:mm:tt"),               
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
