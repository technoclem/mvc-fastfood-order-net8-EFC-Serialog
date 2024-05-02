namespace FastFoodEFC.Dto
{
    public class LoginResponse
    {
        public int CustId { get; set; }
        public string? CustPassword { get; set; }
        public string? ActivatedPin { get; set; }

        public int Activated { get; set; } = 0;
    }
}
