using Grupp9WebbShop.Data.Models;
using System;
using System.Collections.Generic;

namespace Grupp9WebbShop.Web.Models
{
    public class LayoutModel
    {
        public bool ShowNavigation { get; internal set; }
        public IEnumerable<ProductCategory> Categories { get; internal set; }
        public ShoppingBasket ShoppingBasket { get; internal set; }
    }
}