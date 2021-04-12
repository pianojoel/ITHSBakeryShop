using Grupp9WebbShop.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grupp9WebbShop.Data
{
    public interface IShopDataService
    {
        Task<Product> GetProductByIdAsync(int id);
        Product GetProductById(int id);
        Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync();
        IEnumerable<ProductCategory> GetProductCategories();
        IEnumerable<Product> GetProducts();
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Product>> GetHighlightedProductsAsync();
        Task<IEnumerable<Product>> GetProductsOnSaleAsync();
        Task<int> GetProductStockAsync(int id);
        void SetProductStock(int id, int quant);
        Task<bool> DecreaseProductStockAsync(int id, int quant);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        int SaveOrder(Order newOrder);
        Order CreateOrderFromBasket(ShoppingBasket basket, string userId, ShippingTypes shipping, PaymentTypes payment);
        Task<IEnumerable<Order>> GetOrdersForUserAsync(string userId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Product>> GetLatestProductsAsync();
        Order GetOrder(int id);
        IEnumerable<BestSellingProduct> GetBestSellingProducts();
        void ToggleOrderIsProcessed(int id);
        Task<IEnumerable<Tag>> GetTagsAsync();
        Task<List<SelectListItem>> GetTagsListAsync();
        IEnumerable<Product> FilteredProducts(IEnumerable<Product> prods, List<SelectListItem> filter);
    }
}