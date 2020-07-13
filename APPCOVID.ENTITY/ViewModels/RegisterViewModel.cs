using APPCOVID.Entity.DTO;

namespace APPCOVID.Entity.ViewModels
{
    public class RegisterViewModel
    {
        public UserAccountModel UserAccount { get; set; }
        public UserInformationViewModel UserInfo { get; set; }
        public string UserType { get; set; }
    }


    public class UserAccountModel : UserAccountDto { }
    public class UserInformationViewModel : UserInfoDto { }
}
