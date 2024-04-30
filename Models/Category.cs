using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class Category
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Category Name")]
        public required string CatName { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Category Date")]
        public required string CatDate { get; set; }
    }
}
