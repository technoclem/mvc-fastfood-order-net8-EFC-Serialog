using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastFoodEFC.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [DisplayName("Admin ID")]
        public  int AdminId { get; set; }
               
        [Required(ErrorMessage = "Role Required")]
        [DisplayName("Role")]
        [MaxLength(50)]
        public required string AdminRole { get; set; }

        public Admin? Admin { get; set; }
    }
}
