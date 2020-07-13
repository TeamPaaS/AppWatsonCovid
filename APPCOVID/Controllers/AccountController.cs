using APPCOVID.BAL.Helpers.Account;
using APPCOVID.Entity.ViewModels;
using APPCOVID.Models.Session;
using Microsoft.AspNetCore.Mvc;
using System;

namespace APPCOVID.Controllers
{
    public class AccountController : Controller
    {
        private AccountHelper _accountHelper;

        public AccountController()
        {
            _accountHelper = AccountHelper._getInstance;
            #region MyRegion
            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.UserAccount = new UserAccountModel
            {
                LASTLOGINDATE = DateTime.Now.ToString("ddMMyyyyhhmmtt"),
                PASSWORD = "p@a$#Hackath0n",
                USERNAME = "paas@superadmin.com",
                ACTIVATEAC = 1
            };
            registerViewModel.UserInfo = new UserInformationViewModel { NAME = "Administrator", EMAIL = "paas@superadmin.com", PHONENO = "9143417211" };
            registerViewModel.UserType = "admin";
            _accountHelper.CreateAdminUser(registerViewModel);
            #endregion
        }

        public IActionResult Index()
        {
            return View("~/Views/Account/Signin.cshtml");
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            int userId = -1;
            try
            {
                if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
                {
                    return RedirectToActionPermanent("Index", "Account");
                }
                else
                {
                    if (_accountHelper.Login(model, out userId))
                    {
                        HttpContext.Session.SetObject("coviduserid", userId.ToString());
                        return RedirectToActionPermanent("Index", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToActionPermanent("Index", "Account");
        }

        public IActionResult Register(string errorMsg = null)
        {
            ViewBag.ErrMsg = errorMsg;
            return View("~/Views/Account/Signup.cshtml");
        }

        [HttpPost]
        public IActionResult RegisterUser(RegisterInfoViewModel register)
        {
            if (_accountHelper.IsEmailExists(register.Email)){
                return Register("Email already exists");
            }
            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.UserAccount = new UserAccountModel
            {
                LASTLOGINDATE = DateTime.Now.ToString("ddMMyyyyhhmmtt"),
                PASSWORD = register.Password,
                USERNAME = register.Email,
                ACTIVATEAC = 1
            };
            registerViewModel.UserInfo = new UserInformationViewModel { NAME = register.FullName, EMAIL = register.Email, PHONENO = register.Phone };
            registerViewModel.UserType = "citizen";
            _accountHelper.Register(registerViewModel);
            return RedirectToActionPermanent("Index", "Account");
        }

        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToActionPermanent("Index", "Account");
        }


    }
}
