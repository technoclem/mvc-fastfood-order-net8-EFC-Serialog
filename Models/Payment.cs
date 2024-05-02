using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastFoodEFC.Models
{
    public class Payment
    {
        [Key]
        [DisplayName("Payment ID")]
        public  int PaymentId { get; set; }

        [DisplayName("Customer ID")]
        public  int CustId { get; set; }

        [DisplayName("Order ID")]
        public  int OrderId { get; set; }

        [DisplayName("Total Price")]
        public double TotalPrice { get; set; }

        [Required(ErrorMessage = "Payment Date")]
        [DisplayName("Payment date")]
        [MaxLength(50)]
        public string? PDate { get; set; }


        [DisplayName("Dispatch")]
        public int Dispatch { get; set; } = 0;

        public Order? Order { get; set; }

    }
}
