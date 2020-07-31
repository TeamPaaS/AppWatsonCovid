using System.ComponentModel.DataAnnotations;

namespace APPCOVID.Entity.ViewModels
{
    public class RegisterInfoViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public string FullName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        [StringLength(10, ErrorMessage = "Phone length must be 10 digit.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only number allowed")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public string Email { get; set; }

        [StringLength(8, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public string Password { get; set; }

        [StringLength(8, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "*")]
        public string ConfirmPassword { get; set; }
    }
}
