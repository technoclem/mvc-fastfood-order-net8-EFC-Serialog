using System.ComponentModel.DataAnnotations;

namespace FastFood.Dto
{
    public class CartItemViewModel
    {
        public int FoodId { get; set; }
        public float Price { get; set; }

        [Range(1, 100, ErrorMessage = "Quantity must be in the range of 1-100")]
        public int Quantity { get; set; } = 1;
    }
}
