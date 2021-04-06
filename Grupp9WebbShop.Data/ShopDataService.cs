using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Grupp9WebbShop.Data.Models;
using System.Web.Mvc;

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
        public IEnumerable<Product> GetProductsOnSale()
        {
            return _ctx.Products.Where(p => p.OnSale).Include(i => i.Category).ToList();
        }
        public IEnumerable<Product> GetLatestProducts()
        {
            return _ctx.Products.Include(i => i.Category).OrderByDescending(d => d.AddedDate).Take(3).ToList();
        }

        public int GetProductStock(int id)
        {
            var q = _ctx.Inventory.Where(p => p.ProductId == id).FirstOrDefault().Quantity;
            return q;
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
        public bool DecreaseProductStock(int id, int quant)
        {

            var q = _ctx.Inventory.Where(p => p.ProductId == id).FirstOrDefault();
            if (q == null || (q.Quantity - quant < 0)) return false;
            q.Quantity -= quant;
            _ctx.SaveChanges();
            return true;
        }
        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return _ctx.ProductCategories.OrderBy(o => o.Name).ToList();
        }
        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            ProductCategory category = _ctx.ProductCategories.Where(i => i.Id == categoryId).FirstOrDefault();
            var prods = _ctx.Products.Where(c => c.CategoryId == category.Id);
            return prods.OrderBy(o => o.Name).ToList();
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

        public IEnumerable<Order> GetOrdersForUser(string userId)
        {
            return _ctx.Orders.Where(o => o.UserID == userId).ToList();
        }
        public IEnumerable<Order> GetAllOrders()
        {
            return _ctx.Orders.ToList();
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
            }).OrderByDescending(o => o.Count).ToList();
        }
        public void ToggleOrderIsProcessed(int id)
        {
            var order = _ctx.Orders.Find(id);
            order.IsProcessed = !order.IsProcessed;
            _ctx.SaveChanges();
        }
        public IEnumerable<Tag> GetTags()
        {
            return _ctx.Tags.ToList();
        }
        public List<SelectListItem> GetTagsList()
        {
            var tags = GetTags();
            List<SelectListItem> items = new();
            foreach (var t in tags)
            {
                items.Add(new SelectListItem()
                {
                    Text = t.Name,
                    Value = t.Id.ToString()
                });
            }
            return items;
        }
    }
}
