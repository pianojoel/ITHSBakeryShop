﻿using System;
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
            ProductCategory category = _ctx.ProductCategories.Include(i => i.Products).Where(i => i.Id == categoryId).FirstOrDefault();
            return category.Products.OrderBy(o => o.Name).ToList();
        }
        public Product GetProductById(int id)
        {
            return _ctx.Products.Find(id);
        }
        public Order CreateOrderFromBasket(ShoppingBasket basket, string userId, ShippingTypes shipping, PaymentTypes payment)
        {
            Order order = new()
            {
                Date = DateTime.Now,
                UserID = userId,
                PaymentType = payment,
                ShippingType = shipping
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
    }
}
