using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastFoodEFC.Models
{
    public class FoodItem
    {
        [Key]
        [DisplayName("Food Id")]
        public int FoodId { get; set; }

        [Required(ErrorMessage ="Food Name Required")]
        [DisplayName("Food Name")]
        [MaxLength(100)]
        public required string FoodName { get; set; }

        [Required(ErrorMessage = "Food Description Required")]
        [DisplayName("Food Description")]
        [MaxLength(250)]
        public required string FoodDesc { get; set; }


        [Required(ErrorMessage = "Food Price Required")]
        [DisplayName("Food Price")]        
        public double Price { get; set; }

       
        [Required(ErrorMessage = "Stock date required")]
        [DisplayName("Stock date")]
        [MaxLength(50)]
        public string? DateAdded { get; set; }

        [DisplayName("Category ID")]
        public int CatId { get; set; }

        public Category? Category { get; set; }

        public List<Cart>? Carts { get; set; }
        public List<Order>? Orders { get; set; }

    }
}
