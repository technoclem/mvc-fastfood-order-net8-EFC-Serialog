using System.ComponentModel.DataAnnotations;

namespace FastFoodEFC.Dto
{
    public class Shop
    {
        public int FoodId { get; set; }
        public string? FoodName { get; set; }
        public double Price { get; set; }

        public string? CatName { get; set; }
        public string? FoodDescription { get; set; }     

        
    }
}
