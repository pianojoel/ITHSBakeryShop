using Grupp9WebbShop.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grupp9WebbShop.Data
{
    public interface IShopDataService
    {
        Product GetProductById(int id);
        IEnumerable<ProductCategory> GetProductCategories();
        IEnumerable<Product> GetProducts();
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Product>> GetHighlightedProductsAsync();
        Task<IEnumerable<Product>> GetProductsOnSaleAsync();
        int GetProductStock(int id);
        void SetProductStock(int id, int quant);
        bool DecreaseProductStock(int id, int quant);
        IEnumerable<Product> GetProductsByCategory(int categoryId);
        int SaveOrder(Order newOrder);
        Order CreateOrderFromBasket(ShoppingBasket basket, string userId, ShippingTypes shipping, PaymentTypes payment);
        IEnumerable<Order> GetOrdersForUser(string userId);
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Product> GetLatestProducts();
        Order GetOrder(int id);
        IEnumerable<BestSellingProduct> GetBestSellingProducts();
        void ToggleOrderIsProcessed(int id);
        IEnumerable<Tag> GetTags();
        List<SelectListItem> GetTagsList();
        IEnumerable<Product> FilteredProducts(IEnumerable<Product> prods, List<SelectListItem> filter);
    }
}