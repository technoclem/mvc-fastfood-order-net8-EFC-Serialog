using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FastFoodEFC.Dto
{

   
    public class CartList
    {
        
        public int FoodId { get; set; }

        public  string? FoodName { get; set; }
        public string? FoodDescription { get; set; }
        public  double Price { get; set; }

        [Required(ErrorMessage = "Quantity Required")]
        [Display(Name = "Quantity", Prompt = "Quantity")]
        [Range(1, 100, ErrorMessage = "Quantity must be in the range of 1-100")]
        public  int Quantity { get; set; }
    }
}
