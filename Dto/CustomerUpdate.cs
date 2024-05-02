using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FastFoodEFC.Dto
{

   
    public class CustomerUpdate
    {

        [Display(Name = "Customer ID")]
        public int CustId { get; set; } = -1;

        [Required(ErrorMessage = "Full Name Required")]
        [Display(Name = "Full Name", Prompt = "Enter your full name")]
        [MaxLength(200)]
        public string? CustName { get; set; }


        [Required(ErrorMessage = "Address Required")]
        [Display(Name = "Address", Prompt = "Enter your address")]
        [MaxLength(500)]
        public  string? CustAddress { get; set; }

        [Required(ErrorMessage = "Phone Number Required")]
        [Display(Name = "Phone Number", Prompt = "Enter your phone number")]
        [MaxLength(50)]
        public string? CustPhone { get; set; }


        [Required(ErrorMessage = "Email Address Required")]
        [Display(Name = "Email Address", Prompt = "Enter your email address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(100)]
        public  string? CustEmail { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DisplayName("Password")]
        [MaxLength(15)]
        [DataType(DataType.Password)]
        public string? CustPassword { get; set; }

       
    }
}
