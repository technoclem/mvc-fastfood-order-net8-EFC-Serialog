namespace FastFood.Dto
{
    public class CustomerCart
    {
        public int CustId { get; set; }

        public string? CustName { get; set; }

        public string? CustAddress { get; set; }

        public string? CustPhone { get; set; }

        public string? CustEmail { get; set; }
               
        public float Ewallet { get; set; }

        public IEnumerable<Cart>? CustCart { get; set; }

    }

    public class Cart
    {
        public int FoodID { get; set; }

        public string? FoodName { get; set; }

        public float FoodPrice { get; set; }

        public int Quantity { get; set; }

        public string? CartDate { get; set; }

    }
}
