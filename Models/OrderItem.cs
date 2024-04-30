using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class OrderItem
    {
        [Key]
        [DisplayName("Order ID")]
        public int OrderID { get; set; }

        [DisplayName("Customer ID")]
        public required int CustID { get; set; }

        [DisplayName("Food ID")]
        public required int FoodID { get; set; }

        [DisplayName("Quantity")]
        public required int Quantity { get; set; }

       
        [Required(ErrorMessage = "Food Price Required")]
        [DisplayName("Food Price")]        
        public required float FoodPrice { get; set; }

       
        [Required(ErrorMessage = "Order date required")]
        [DisplayName("Order date")]
        [MaxLength(50)]
        public string? Odate { get; set; }

       
    }
}
