using FastFoodEFC.Dto;

namespace FastFood.Service.Interface
{
    public interface IFoodService
    {
       
        Task<CartList?> GetProductDetails(int Id);
        Task<IEnumerable<RecentItem>?> GetRecentItem();
        Task<IEnumerable<Shop>?> GetShopList(string SearchString);

    }
}
