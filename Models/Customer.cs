using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastFoodEFC.Models
{
    public class Customer
    {
        [Key]
        public int CustId { get; set; }

        [Required(ErrorMessage ="Full Name Required")]
        [DisplayName("Full Name")]
        [MaxLength(100)]
        public  string? CustName { get; set; }


        [Required(ErrorMessage = "Customer Address Required")]
        [DisplayName("Customer Address")]
        [MaxLength(100)]
        public  string? CustAddress { get; set; }

        [Required(ErrorMessage = "Customer Phone Number Required")]
        [DisplayName("Customer Phone")]
        [MaxLength(30)]
        public string? CustPhone { get; set; }


        [Required(ErrorMessage = "Email Address Required")]
        [DisplayName("Email Address")]
        [MaxLength(100)]
        public string? CustEmail { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DisplayName("Password")]
        [MaxLength(20)]
        public string? CustPassword { get; set; }

        [Required(ErrorMessage = "Registration date required")]
        [DisplayName("Registration date")]
        [MaxLength(20)]
        public string? RegDate { get; set; }

        
        [DisplayName("Activated Pin")]
        [MaxLength(20)]
        public string? ActivatedPin { get; set; }

        [DisplayName("Activated ?")]
        [MaxLength(1)]
        public int Activated { get; set; } = 0;

        [DisplayName("Ewallet")]
        public double EWallet { get; set; } = 2000f;

        public List<Cart>? Carts { get; set; }
        public List<Order>? Orders { get; set; }

    }
}
