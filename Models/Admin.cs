using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastFoodEFC.Models
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }

        [Required(ErrorMessage ="Full Name Required")]
        [DisplayName("Full Name")]
        [MaxLength(100)]
        public required string FullName { get; set; }

        [Required(ErrorMessage = "Email Address Required")]
        [DisplayName("Email Address")]
        [MaxLength(100)]
        public required string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DisplayName("Password")]
        [MaxLength(20)]
        public required string AdminPassword { get; set; }

        [Required(ErrorMessage = "Registration date required")]
        [DisplayName("Registration date")]
        [MaxLength(20)]
        public required string RegDate { get; set; }

        public List<Role>? Roles { get; set; }
    }
}
