using FastFood.Dto;

namespace FastFood.Service.Interface
{
    public interface IFoodService
    {
        Task <IEnumerable<CategoryList>?> GetCategoryList();
        Task<CartList?> GetProductDetails(int Id);
        Task<IEnumerable<RecentItem>?> GetRecentItem();
        Task<IEnumerable<Shop>?> GetShopList(string SearchString);

    }
}
