using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Grupp9WebbShop.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grupp9WebbShop.Data
{
    public class ShopDataService : IShopDataService
    {
        private readonly ShopContext _ctx;

        public ShopDataService(ShopContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _ctx.Products.Include(i => i.Category).Include(a => a.AllergyTags).ToListAsync();
        }
    public IEnumerable<Product> GetProducts()
        {
            return _ctx.Products.Include(i => i.Category).Include(a => a.AllergyTags).ToList();
        }

        public async Task<IEnumerable<Product>> GetHighlightedProductsAsync()
        {
            return await _ctx.Products.Where(h => h.Highlighted).Include(i => i.Category).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsOnSaleAsync()
        {
            return await _ctx.Products.Where(p => p.OnSale).Include(i => i.Category).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetLatestProductsAsync()
        {
            return await _ctx.Products.Include(i => i.Category).OrderByDescending(d => d.AddedDate).Take(3).ToListAsync();
        }

        public async Task<int> GetProductStockAsync(int id)
        {
            var q = await _ctx.Inventory.Where(p => p.ProductId == id).FirstOrDefaultAsync();

            return q.Quantity;
        }
        public void SetProductStock(int id, int quant)
        {
            var q = _ctx.Inventory.Where(p => p.ProductId == id).FirstOrDefault();
            if (q == null)
            {
                q = new InventoryItem()
                {
                    ProductId = id,
                    Quantity = quant
                };
                _ctx.Inventory.Add(q);
            }
            else
            {
                q.Quantity = quant;
            }
            _ctx.SaveChanges();
        }
        public async Task<bool> DecreaseProductStockAsync(int id, int quant)
        {

            var q = await _ctx.Inventory.Where(p => p.ProductId == id).FirstOrDefaultAsync();
            if (q == null || (q.Quantity - quant < 0)) return false;
            q.Quantity -= quant;
            await _ctx.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync()
        {
            return await _ctx.ProductCategories.OrderBy(o => o.DisplayOrder).ToListAsync();
        }
        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return _ctx.ProductCategories.OrderBy(o => o.DisplayOrder).ToList();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            ProductCategory category = await _ctx.ProductCategories.Where(i => i.Id == categoryId).FirstOrDefaultAsync();
            var prods = _ctx.Products.Where(c => c.CategoryId == category.Id);
            return await prods.OrderBy(o => o.Name).ToListAsync();
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _ctx.Products.Include(c => c.Category).Include(t => t.AllergyTags).FirstOrDefaultAsync(i => i.Id == id);
        }
        public Product GetProductById(int id)
        {
            return _ctx.Products.Include(c => c.Category).Include(t => t.AllergyTags).FirstOrDefault(i => i.Id == id);
        }

        public Order CreateOrderFromBasket(ShoppingBasket basket, string userId, ShippingTypes shipping, PaymentTypes payment)
        {
            Order order = new()
            {
                Date = DateTime.Now,
                UserID = userId,
                PaymentType = payment,
                ShippingType = shipping,
                IsProcessed = false
            };
            foreach (var item in basket.Items)
            {
                OrderItem oi = new()
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                order.OrderItems.Add(oi);
            }
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersForUserAsync(string userId)
        {
            return await _ctx.Orders.Where(o => o.UserID == userId).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _ctx.Orders.ToListAsync();
        }

        public int SaveOrder(Order newOrder)
        {
            _ctx.Orders.Add(newOrder);
            _ctx.SaveChanges();
            return newOrder.Id;
        }
        public Order GetOrder(int id)
        {
            return _ctx.Orders.Include(o => o.OrderItems).FirstOrDefault(i => i.Id == id);
        }
        public IEnumerable<BestSellingProduct> GetBestSellingProducts()
        {
            return _ctx.OrderItems.GroupBy(i => i.ProductId, (g, p) => new BestSellingProduct()
            {
                ProductId = g,
                Count = p.Sum(s => s.Quantity)
            }).OrderByDescending(o => o.Count).Take(3).ToList();
        }
        public void ToggleOrderIsProcessed(int id)
        {
            var order = _ctx.Orders.Find(id);
            order.IsProcessed = !order.IsProcessed;
            _ctx.SaveChanges();
        }
        public async Task<IEnumerable<Tag>> GetTagsAsync()
        {
            return await _ctx.Tags.ToListAsync();
        }
        public async Task<List<SelectListItem>> GetTagsListAsync()
        {
            var tags = await GetTagsAsync();
            List<SelectListItem> items = new();
            foreach (var t in tags)
            {
                items.Add(new SelectListItem()
                {
                    Text = t.Name,
                    Value = t.Id.ToString(),
                    Selected = true
                });
            }
            return items;
        }
        public IEnumerable<Product> FilteredProducts(IEnumerable<Product> prods, List<SelectListItem> filter)
        {
            List<Product> filteredList = new();
            filteredList.AddRange(prods);

            foreach (var f in filter)
            {
                if (f.Selected) continue;
                var tagId = int.Parse(f.Value);
                var removeList = prods.Where(t => t.AllergyTags.Select(i => i.Id).Contains(tagId));
                foreach (var item in removeList)
                {
                    filteredList.Remove(item);
                }
            }
            return filteredList;
        }
    }
}
