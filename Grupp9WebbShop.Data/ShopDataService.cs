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
        public IEnumerable<Product> GetHighlightedProducts()
        {
            return _ctx.Products.Where(h => h.Highlighted).Include(i => i.Category).ToList();
        }

        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return _ctx.ProductCategories.OrderBy(o => o.Name).ToList();
        }
        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            ProductCategory category = _ctx.ProductCategories.Include(i => i.Products).Where(i => i.Id == categoryId).FirstOrDefault();
            return category.Products.OrderBy(o => o.Name).ToList();
        }
        public Product GetProductById(int id)
        {
            return _ctx.Products.Find(id);
        }
    }
}
