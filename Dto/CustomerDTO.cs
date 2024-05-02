using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FastFoodEFC.Dto
{


    public class CustomerDTO
    {
        public int CustId { get; set; }

        public string? CustName { get; set; }

        public string? CustAddress { get; set; }

        public string? CustPhone { get; set; }

        public string? CustEmail { get; set; }

        public string? CustPassword { get; set; }

        public double Ewallet { get; set; }

        public IEnumerable<CustOrder>? CustOrder { get; set;}

    }

    public class CustOrder
    {
        public string? OrderDate { get; set; }

        public int FoodID { get; set; }

        public string? FoodName { get; set; }

        public double FoodPrice { get; set; }

        public int Quantity { get; set; }

    }
}
