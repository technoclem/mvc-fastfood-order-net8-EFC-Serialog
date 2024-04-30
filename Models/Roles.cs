using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class Roles
    {
        [Key]
        [DisplayName("Admin ID")]
        public  int AdminID { get; set; }
               
        [Required(ErrorMessage = "Role Required")]
        [DisplayName("Role")]
        [MaxLength(50)]
        public required string AdminRole { get; set; }       
    }
}
