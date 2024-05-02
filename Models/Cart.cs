using System.ComponentModel.DataAnnotations;

namespace FastFoodEFC.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public int CustId { get; set; }

        public int FoodId { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        [Required]
        [StringLength(100)]
        public string? CartDate { get; set; }

        public Customer? Customer { get; set; } // Navigation property
        public FoodItem? FoodItem { get; set; }

    }
}
