using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class SignUpViewModel
    {
        [Required (ErrorMessage = "First Name Is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage ="Invalid Format of Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^(?=(.*[a-z]){1})(?=(.*[A-Z]){1})(?=(.*\d){1})(?=(.*[^\w\d]){2}).{6,}$",
                ErrorMessage = "The password must contain at least 1 lowercase letter, 1 uppercase letter, 1 digit, and 2 unique characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword Is Required")]
        [Compare(nameof(Password) , ErrorMessage ="Confirm Password does not match Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Required To Agree")]
        public bool IsAgree { get; set; }
    }
}
