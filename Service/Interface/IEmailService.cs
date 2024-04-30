namespace FastFood.Service.Interface
{
    public interface IEmailService
    {
        Task<string> GetHTMLTemplate(string subject, string body);
        Task SendMail(string subject, string body, string receiver);
       
    }
}
