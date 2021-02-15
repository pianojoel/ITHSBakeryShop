using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grupp9WebbShop.Data.Models
{
    public class ShoppingBasket
    {
        public List<ShoppingBasketItem> Items { get; set; } = new();
        public decimal Total
        {
            get
            {
                return Items.Sum(s => s.RowPrice);
            }
        }
    }
}
