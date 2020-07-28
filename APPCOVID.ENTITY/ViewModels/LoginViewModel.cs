using System.ComponentModel.DataAnnotations;

namespace APPCOVID.Entity.ViewModels
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public string Password { get; set; }
    }
}
