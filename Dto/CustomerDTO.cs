using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FastFood.Dto
{


    public class CustomerDTO
    {
        public int CustId { get; set; }

        public string? CustName { get; set; }

        public string? CustAddress { get; set; }

        public string? CustPhone { get; set; }

        public string? CustEmail { get; set; }

        public string? CustPassword { get; set; }

        public float Ewallet { get; set; }

        public IEnumerable<Order>? CustOrder { get; set;}

    }

    public class Order
    {
        public string? OrderDate { get; set; }

        public int FoodID { get; set; }

        public string? FoodName { get; set; }

        public float FoodPrice { get; set; }

        public int Quantity { get; set; }

    }
}
