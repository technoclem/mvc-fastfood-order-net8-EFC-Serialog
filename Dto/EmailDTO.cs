using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FastFoodEFC.Dto
{   
    public class EmailDTO
    {


        [Required(ErrorMessage = "Email Address Required")]
        [Display(Name = "Email Address", Prompt = "Enter your email address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(100)]
        public  string? CustEmail { get; set; }
       
    }
}
