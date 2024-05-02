namespace FastFoodEFC.Dto
{
    public class Header
    {
        public IEnumerable<CartList>? CartList{ get; set; }

        public CustProfileName? CustProfileName { get; set;}

        public IEnumerable<CategoryList>? CategoryList { get; set; }
    }
}
