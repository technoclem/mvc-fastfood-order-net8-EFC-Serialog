using System.ComponentModel.DataAnnotations;

namespace FastFood.Dto
{
    public class Shop
    {
        public int FoodId { get; set; }
        public string? FoodName { get; set; }
        public float Price { get; set; }

        public string? CatName { get; set; }
        public string? FoodDescription { get; set; }     

        
    }
}
