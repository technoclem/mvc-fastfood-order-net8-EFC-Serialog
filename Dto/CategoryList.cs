using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FastFoodEFC.Dto
{
    public class CategoryList
    {
       
       public  required int  CatId { get; set; }

       
        public required string CatName { get; set; }
    }
}
