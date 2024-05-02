using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastFoodEFC.Models
{
    public class Category
    {
        [Key]
        public int CatId { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Category Name")]
        public required string CatName { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Category Date")]
        public required string CatDate { get; set; }

        public List<FoodItem>? FoodItems { get; set; }

    }
}
