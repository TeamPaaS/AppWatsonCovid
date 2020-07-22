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
            _accontHelper = AccountHelper._getInstance;
            string userId = HttpContext.Session.GetObject("coviduserid");
            if (string.IsNullOrEmpty(userId))
            {
                RedirectToActionPermanent("Logout", "Account");
            }
            CurrentUserId = Convert.ToInt32(userId);
            int roleId = _accontHelper.GetRoleByUserid(Convert.ToInt32(userId));
            if (roleId < 1)
            {
                RedirectToActionPermanent("Logout", "Account");
            }
            if (roleId == 1) {
                IsUserInAdminRole = true;
            }
            ViewBag.roleId = roleId;
            UserInformationViewModel userInfo = _accontHelper.UserDataByUserId(Convert.ToInt32(userId));
            ViewBag.fullName = string.IsNullOrEmpty(userInfo.NAME) ? "Unknown" : userInfo.NAME;
        }

        public void Authorize(string role)
        {
            _accontHelper = AccountHelper._getInstance;
            string userId = HttpContext.Session.GetObject("coviduserid");
            if (string.IsNullOrEmpty(userId))
            {
                RedirectToActionPermanent("Logout", "Account");
            }
            CurrentUserId = Convert.ToInt32(userId);
            int roleId = _accontHelper.GetRoleByUserid(Convert.ToInt32(userId));
            if (roleId < 1)
            {
                RedirectToActionPermanent("Logout", "Account");
            }
            else if (role.ToLower() == "admin" && roleId!=1)
            {
                RedirectToActionPermanent("Logout", "Account");
            }
            else if (role.ToLower() == "customer" && roleId != 2)
            {
                RedirectToActionPermanent("Logout", "Account");
            }
            else if (role.ToLower() == "citizen" && roleId != 3)
            {
                RedirectToActionPermanent("Logout", "Account");
            }
            if (roleId == 1)
            {
                IsUserInAdminRole = true;
            }
            ViewBag.roleId = roleId;
            UserInformationViewModel userInfo = _accontHelper.UserDataByUserId(Convert.ToInt32(userId));
            ViewBag.fullName = string.IsNullOrEmpty(userInfo.NAME) ? "Unknown" : userInfo.NAME;
        }
    }
}
