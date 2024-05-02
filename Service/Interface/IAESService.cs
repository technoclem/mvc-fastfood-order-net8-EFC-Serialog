using FastFoodEFC.Dto;

namespace FastFood.Service.Interface
{
    public interface IAESService
    {
        Task<string> Encrypt(string text);
        Task<string> Decrypt(string text);
    }
}
