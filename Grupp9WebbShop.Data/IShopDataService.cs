using Grupp9WebbShop.Data.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Grupp9WebbShop.Data
{
    public interface IShopDataService
    {
        Product GetProductById(int id);
        IEnumerable<ProductCategory> GetProductCategories();
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetHighlightedProducts();
        IEnumerable<Product> GetProductsOnSale();
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
    }
}