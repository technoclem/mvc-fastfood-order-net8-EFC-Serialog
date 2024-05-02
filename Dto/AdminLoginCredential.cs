using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FastFoodEFC.Dto
{
    public class AdminLoginCredential
    {
        [Required(ErrorMessage = "Email Address Required")]
        [Display(Name = "Email Address", Prompt = "Enter your email address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(100)]
        public string? CustEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password Required")]
        [DisplayName("Password")]
        [MaxLength(15)]
        [DataType(DataType.Password)]
        public string? CustPassword { get; set; } = string.Empty;
    }
}
