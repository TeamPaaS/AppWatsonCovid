using APP27062020.DAL.DataManagers;
using APPCOVID.DAL.DataManagers.Managers;
using APPCOVID.Entity.DTO;
using APPCOVID.Entity.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace APPCOVID.BAL.Helpers.Account
{
    public class AccountHelper
    {
        private RoleManager _roleManager;
        private UserAccountManager _userAccountManager;
        private UserInfoManager _userInfoManager;
        private UserRoleMapManager _userRoleMapManager;

        public static AccountHelper _getInstance
        {
            get
            {
                return new AccountHelper();
            }
        }

        private AccountHelper()
        {
            _roleManager = new RoleManager();
            _userAccountManager = new UserAccountManager();
            _userInfoManager = new UserInfoManager();
            _userRoleMapManager = new UserRoleMapManager();

            #region Create Role
            //_roleManager.CreateRole(new RoleDto { ROLENAME = "admin" });
            //_roleManager.CreateRole(new RoleDto { ROLENAME = "customer" });
            //_roleManager.CreateRole(new RoleDto { ROLENAME = "citizen" });
            //Register(new RegisterViewModel { })
            #endregion
        }

        #region UserAccount 

        public bool Login(LoginViewModel loginModel, out int userId)
        {
            userId = 0;
            bool loginStats = false;
            UserAccountDto loginData = _userAccountManager.GetUserLoginData(loginModel.UserName, loginModel.Password);
            if (loginData != null)
            {
                userId = loginData.USERID;
                 loginStats = !string.IsNullOrEmpty(loginData.USERNAME) && loginData.ACTIVATEAC == 1;
            }
            return loginStats;
        }

        public bool Register(RegisterViewModel registerModel)
        {
            UserAccountDto userAccountDto = CommonHelper.ConvertTo<UserAccountDto>(registerModel.UserAccount);
            if (_userAccountManager.CreateUserAccount(userAccountDto))
            {
                UserAccountDto loginData = _userAccountManager.GetUserLoginData(registerModel.UserAccount.USERNAME, registerModel.UserAccount.PASSWORD);
                registerModel.UserInfo.USERID = loginData.USERID;

                UserRoleMapDto userRoleMapDto = new UserRoleMapDto();
                switch (registerModel.UserType)
                {
                    case "admin":
                        {
                            userRoleMapDto.USERID = loginData.USERID;
                            userRoleMapDto.ROLEID = 1;
                            break;
                        }
                    case "customer":
                        {
                            userRoleMapDto.USERID = loginData.USERID;
                            userRoleMapDto.ROLEID = 2;
                            break;
                        }
                    case "citizen":
                        {
                            userRoleMapDto.USERID = loginData.USERID;
                            userRoleMapDto.ROLEID = 3;
                            break;
                        }
                }

                bool isUserRoleCreated = _userRoleMapManager.CreateUserRoleMap(userRoleMapDto);
                if (isUserRoleCreated)
                {
                    return CreateUserInformation(registerModel.UserInfo);
                }
            }
            return false;
        }



        #endregion

        #region User Information
        public bool CreateUserInformation(UserInformationViewModel userInformationViewModel)
        {
            UserInfoDto userInfoDto = CommonHelper.ConvertTo<UserInfoDto>(userInformationViewModel);
            return _userInfoManager.CreateUserInfo(userInfoDto);
        }

        public UserInformationViewModel UserDataByUserId(int uid)
        {
            List<UserInfoDto> getUserInfo = _userInfoManager.GetUserInfoData();
            UserInfoDto userDetails = getUserInfo.Where(t => t.USERID == uid).FirstOrDefault();
            UserInformationViewModel userInfo = CommonHelper.ConvertTo<UserInformationViewModel>(userDetails);
            return userInfo;
        }

        public void CreateAdminUser(RegisterViewModel registerModel)
        {
            UserAccountDto loginData = _userAccountManager.GetUserLoginData(registerModel.UserAccount.USERNAME, registerModel.UserAccount.PASSWORD);
            if (loginData == null || loginData.USERID == null || loginData.USERID == 0)
                Register(registerModel);
        }

        #endregion

        #region Role
        public bool IsUserInRole(int uid, int roleId)
        {
            List<UserRoleMapDto> userRoleMaps = _userRoleMapManager.GetUserRoleMapData();
            UserRoleMapDto roles = userRoleMaps.Where(t => t.USERID == uid).Select(t => t).FirstOrDefault();
            return roles.ROLEID == roleId;
        }

        public int GetRoleByUserid(int uid)
        {
            List<UserRoleMapDto> userRoleMaps = _userRoleMapManager.GetUserRoleMapData();
            UserRoleMapDto roles = userRoleMaps.Where(t => t.USERID == uid).Select(t => t).FirstOrDefault();
            return roles != null ? roles.ROLEID : 0;
        }

        public bool IsEmailExists(string email) {
           return _userAccountManager.GetUserAccountData().Where(t => string.Equals(t.USERNAME, email, System.StringComparison.CurrentCultureIgnoreCase)).Any();
        }


        #endregion
    }
}
