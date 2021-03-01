using Grupp9WebbShop.Data.Models;
using System.Collections.Generic;

namespace Grupp9WebbShop.Data
{
    public interface IShopDataService
    {
        Product GetProductById(int id);
        IEnumerable<ProductCategory> GetProductCategories();
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetHighlightedProducts();
        IEnumerable<Product> GetProductsByCategory(int categoryId);
    }
}