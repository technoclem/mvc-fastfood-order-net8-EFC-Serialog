using FastFoodEFC.Dto;
using FastFood.Service.Interface;
using FastFoodEFC.Data;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Service
{
    public class FoodService:IFoodService
    {
        private IWebHostEnvironment Environment;
        private readonly FastFoodDbContext _context;
        private readonly ILogger<FoodService> _logger;

        public FoodService(IWebHostEnvironment env, FastFoodDbContext dbContext, ILogger<FoodService> logger)
        {
            Environment = env;
            _context = dbContext;
            _logger = logger;
        }

       
       
        public async Task<IEnumerable<RecentItem>?> GetRecentItem()
        {
            try
            {
                var recentItems = await _context.FoodItems
                .OrderByDescending(f => f.FoodId)
                .Take(20)
                .Select(f => new RecentItem { FoodName = f.FoodName, FoodId = f.FoodId, Price = f.Price })
                .ToListAsync();

                return recentItems;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting Recent Item");
                return null;
            }

        }
        public async Task<CartList?> GetProductDetails(int Id)
        {
            try
            {
                var productDetail = await _context.FoodItems
                 .Where(f => f.FoodId == Id)
                 .Select(f => new CartList
                 {
                     FoodId = f.FoodId,
                     FoodName = f.FoodName,
                     FoodDescription = f.FoodDesc,
                     Price = f.Price,
                     Quantity = 1
                 }).FirstOrDefaultAsync();

                return productDetail;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting Product Details with ID: {Id}");
                return null;
            }
        }
        public async Task<IEnumerable<Shop>?> GetShopList(string SearchString)
        {
            try
            {  
                
                var query = _context.FoodItems
                .Include(fi => fi.Category)
                .AsQueryable();

                if (!string.IsNullOrEmpty(SearchString))
                {
                    query = query.Where(fi => fi.FoodName.Contains(SearchString) 
                    || (fi.Category != null && fi.Category.CatName.Contains(SearchString)));
                }

                var result = await query
                    .OrderBy(fi => fi.FoodName)
                    .Select(fi => new Shop
                    {
                        FoodId = fi.FoodId,
                        FoodName = fi.FoodName,
                        Price = fi.Price,
                        CatName = fi.Category != null ? fi.Category.CatName : null,
                        FoodDescription = fi.FoodDesc
                    })
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting Shop List");
                return null;
            }
        }
    }
}
