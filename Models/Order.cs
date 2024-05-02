using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastFoodEFC.Models
{
    public class Order
    {
        [Key]
        [DisplayName("Order ID")]
        public int OrderId { get; set; }

        [DisplayName("Customer ID")]
        public int CustId { get; set; }

        [DisplayName("Food ID")]
        public int FoodId { get; set; }

        [DisplayName("Quantity")]
        public int Quantity { get; set; }


        [Required(ErrorMessage = "Food Price Required")]
        [DisplayName("Food Price")]
        public double Price { get; set; }



        [DisplayName("Order date")]
        [MaxLength(50)]
        public string? OrderDate { get; set; }

        public Customer? Customer { get; set; }
        public FoodItem? FoodItem { get; set; }
        public List<Payment>? Payments { get; set; }

    }


}
