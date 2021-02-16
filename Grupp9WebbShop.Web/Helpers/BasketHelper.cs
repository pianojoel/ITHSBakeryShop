using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grupp9WebbShop.Data.Models;

namespace Grupp9WebbShop.Web.Helpers
{
    public static class BasketHelper
    {
        private static string key = "CustomerBasket";
        public static int NumItems(ISession session)
        {
            var b = GetBasket(session);
            return b.Items.Sum(s => s.Quantity);
        }
        public static ShoppingBasket GetBasket(ISession session)
        {
            ShoppingBasket b = session.GetObjectFromJson<ShoppingBasket>(key);
            if (b == null) b = new();

            return b;
        }
        public static void AddToBasket(ISession session, int prodId, decimal price, int quantity)
        {
            var b = GetBasket(session);
            if (b.Items.Select(i => i.ProductId).Contains(prodId))
                b.Items.Where(p => p.ProductId == prodId).FirstOrDefault().Quantity += quantity;
            else
                b.Items.Add(new ShoppingBasketItem()
                {
ProductId = prodId,
Price = price,
Quantity = quantity
                });
            SaveBasket(session, b);
        }
        public static void RemoveItem(ISession session, int productId)
        {
            var b = GetBasket(session);
            if (b.Items.Count == 0) return;
            var row = b.Items.Where(i => i.ProductId == productId).FirstOrDefault();
            if (row == null) return;
            row.Quantity--;
            if (row.Quantity == 0) b.Items.Remove(row);
            SaveBasket(session, b);
        }
        private static void SaveBasket(ISession session, ShoppingBasket b)
        {
            session.SetObjectAsJson(key, b);
        }
    }
}
