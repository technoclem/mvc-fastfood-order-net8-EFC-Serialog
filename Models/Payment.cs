using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class Payment
    {
        [Key]
        [DisplayName("Payment ID")]
        public  int ID { get; set; }

        [DisplayName("Customer ID")]
        public required int CustID { get; set; }

        [DisplayName("Order ID")]
        public required int OrderID { get; set; }

        [DisplayName("Total Price")]
        public required int totalPrice { get; set; }

        [Required(ErrorMessage = "Payment Date")]
        [DisplayName("Payment date")]
        [MaxLength(50)]
        public string? Pdate { get; set; }


        [DisplayName("Dispatch")]
        public int Dispatch { get; set; } = 0;      
        

       
    }
}
