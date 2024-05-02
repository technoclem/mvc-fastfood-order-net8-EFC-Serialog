using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FastFoodEFC.Dto
{

   
    public class RecentItem
    {
        public required int FoodId { get; set; }

        public required string FoodName { get; set; }
        public required double Price { get; set; }

    }
}
