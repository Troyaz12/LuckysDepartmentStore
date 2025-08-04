using System.ComponentModel.DataAnnotations;

namespace LuckysDepartmentStore.Models
{
    public class RegisterationModel
    {
            [Required]
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Security Question")]
            public string SecurityQuestion { get; set; }

            [Required]
            [Display(Name = "Security Answer")]
            public string SecurityAnswer { get; set; }
    }
}
