using APPCOVID.BAL.Helpers.Account;
using APPCOVID.Entity.ViewModels;
using APPCOVID.Models.Session;
using Microsoft.AspNetCore.Mvc;
using System;

namespace APPCOVID.Controllers.Core
{
    public class CovidController : Controller
    {
        public int RoleId { get; set; }
        public int CurrentUserId { get; set; }
        public AccountHelper _accontHelper;

        public bool IsUserInAdminRole { get; set; }
        public CovidController()
        {

        }

        public void Authorize()
        {
            try
            {
                _accontHelper = AccountHelper._getInstance;
                string userId = HttpContext.Session.GetObject("coviduserid");
                if (string.IsNullOrEmpty(userId))
                {
                    ReturnToLogout();
                }

                CurrentUserId = Convert.ToInt32(userId);
                int roleId = _accontHelper.GetRoleByUserid(Convert.ToInt32(userId));
                if (roleId < 1)
                {
                    ReturnToLogout();
                }

                if (roleId == 1)
                {
                    IsUserInAdminRole = true;
                }

                ViewBag.roleId = roleId;
                UserInformationViewModel userInfo = _accontHelper.UserDataByUserId(Convert.ToInt32(userId));
                ViewBag.fullName = string.IsNullOrEmpty(userInfo.NAME) ? "Unknown" : userInfo.NAME;
            }
            catch
            {
                ReturnToLogout();
            }
        }

        public void Authorize(string role)
        {
            try
            {
                _accontHelper = AccountHelper._getInstance;
                string userId = HttpContext.Session.GetObject("coviduserid");
                if (string.IsNullOrEmpty(userId))
                {
                    ReturnToLogout();
                }

                CurrentUserId = Convert.ToInt32(userId);
                int roleId = _accontHelper.GetRoleByUserid(Convert.ToInt32(userId));
                if (roleId < 1)
                {
                    ReturnToLogout();
                }
                else if (role.ToLower() == "admin" && roleId != 1)
                {
                    ReturnToLogout();
                }
                else if (role.ToLower() == "customer" && roleId != 2)
                {
                    ReturnToLogout();
                }
                else if (role.ToLower() == "citizen" && roleId != 3)
                {
                    
                }

                if (roleId == 1)
                {
                    IsUserInAdminRole = true;
                }

                ViewBag.roleId = roleId;
                UserInformationViewModel userInfo = _accontHelper.UserDataByUserId(Convert.ToInt32(userId));
                ViewBag.fullName = string.IsNullOrEmpty(userInfo.NAME) ? "Unknown" : userInfo.NAME;
            }
            catch
            {
                ReturnToLogout();
            }
        }

        public IActionResult ReturnToLogout()
        {
            return RedirectToAction("Logout", "Account");

        }
    }
}
