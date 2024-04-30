using FastFood.Dto;
using FastFood.Models;

namespace FastFood.Service.Interface
{
    public interface ICustomerService
    {
        Task<IEnumerable<CartList>?> GetCartList(int CustID);
        Task<bool> RemoveCart(int CustID,int FoodID);
        Task<IEnumerable<CartList>?> GetOrderList(int CustID);
        Task<bool> AddToCart(int CustID,CartItemViewModel model);
        Task<CustProfileName?> GetCustomer(int CustID);
        Task<CustomerUpdate?> GetCustomer2(int CustID);
        Task<int> UpdateProfile(CustomerUpdate CustModel);
        Task<bool> UpdateProfileImage(IFormFile ImageFile, int CustID);
        Task<int> GetCustIdFromHttpContext();
        Task<int> SignUp(CustomerSignUp CustModel);
        Task<int> DoesCustEmailExist(string Email);
        Task<int> ConfirmAccount(int CustID, string ActivatedPin);
        Task<int> Login(LoginCredential LoginModel);
        Task<int> RequestPassword(string CustEmail);
        Task<bool> SetLoginCookie(int CustID);
        Task<CustomerDTO?> GetCustomerAccount(int CustID);
        Task<CustomerCart?> CheckOut(int CustID);
        Task<int> PlaceOrder(int CustID);

    }
}
