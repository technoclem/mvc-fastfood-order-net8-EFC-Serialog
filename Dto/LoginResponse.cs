namespace FastFood.Dto
{
    public class LoginResponse
    {
        public string? CustId { get; set; }
        public string? CustPassword { get; set; }
        public string? ActivatedPin { get; set; }

        public int activated { get; set; } = 0;
    }
}
