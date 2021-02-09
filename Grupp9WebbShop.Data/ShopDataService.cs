using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Grupp9WebbShop.Data.Models;
namespace Grupp9WebbShop.Data
{
    public class ShopDataService : IShopDataService
    {
        private readonly ShopContext _ctx;

        public ShopDataService(ShopContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<Product> GetProducts()
        {
            return _ctx.Products.Include(i => i.Category).ToList();
        }
        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return _ctx.ProductCategories.ToList();
        }
        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            ProductCategory category = _ctx.ProductCategories.Find(categoryId);
            return category.Products.ToList();
        }
        public Product GetProductById(int id)
        {
            return _ctx.Products.Find(id);
        }
    }
}
