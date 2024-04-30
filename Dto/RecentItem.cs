using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FastFood.Dto
{

   
    public class RecentItem
    {
        public required int FoodId { get; set; }

        public required string FoodName { get; set; }
        public required float Price { get; set; }

    }
}
