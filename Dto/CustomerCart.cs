namespace FastFoodEFC.Dto
{
    public class CustomerCart
    {
        public int CustId { get; set; }

        public string? CustName { get; set; }

        public string? CustAddress { get; set; }

        public string? CustPhone { get; set; }

        public string? CustEmail { get; set; }
               
        public double Ewallet { get; set; }

        public IEnumerable<CCart>? CustCart { get; set; }

    }

    public class CCart
    {
        public int FoodID { get; set; }

        public string? FoodName { get; set; }

        public double FoodPrice { get; set; }

        public int Quantity { get; set; }

        public string? CartDate { get; set; }

    }
}
