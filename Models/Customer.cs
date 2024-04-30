using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class Customer
    {
        [Key]
        public int CustID { get; set; }

        [Required(ErrorMessage ="Full Name Required")]
        [DisplayName("Full Name")]
        [MaxLength(100)]
        public required string CustName { get; set; }


        [Required(ErrorMessage = "Customer Address Required")]
        [DisplayName("Customer Address")]
        [MaxLength(100)]
        public required string CustAddress { get; set; }

        [Required(ErrorMessage = "Customer Phone Number Required")]
        [DisplayName("Customer Phone")]
        [MaxLength(30)]
        public required string CustPhone { get; set; }


        [Required(ErrorMessage = "Email Address Required")]
        [DisplayName("Email Address")]
        [MaxLength(100)]
        public required string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DisplayName("Password")]
        [MaxLength(20)]
        public required string CustPassword { get; set; }

        [Required(ErrorMessage = "Registration date required")]
        [DisplayName("Registration date")]
        [MaxLength(20)]
        public required string RegDate { get; set; }

        
        [DisplayName("Activated Pin")]
        [MaxLength(20)]
        public string? ActivatedPin { get; set; }

        [DisplayName("Activated ?")]
        [MaxLength(1)]
        public int Activated { get; set; } = 0;

        [DisplayName("Ewallet")]
        [MaxLength(15)]
        public float Ewallet { get; set; } = 2000f;
    }
}
