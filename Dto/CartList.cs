using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FastFood.Dto
{

   
    public class CartList
    {
        
        public required int FoodId { get; set; }

        public required string FoodName { get; set; }
        public string? FoodDescription { get; set; }
        public required float Price { get; set; }

        [Required(ErrorMessage = "Quantity Required")]
        [Display(Name = "Quantity", Prompt = "Quantity")]
        [Range(1, 100, ErrorMessage = "Quantity must be in the range of 1-100")]
        public required int Quantity { get; set; }
    }
}
