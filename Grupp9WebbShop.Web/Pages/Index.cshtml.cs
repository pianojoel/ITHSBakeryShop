using Grupp9WebbShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grupp9WebbShop.Data;
using Grupp9WebbShop.Web.Helpers;
using Grupp9WebbShop.Data.Models;

namespace Grupp9WebbShop.Web.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly IShopDataService _ds;
        public List<Product> Highlighted { get; set; }
        public List<Product> OnSale { get; set; }

        [BindProperty]
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        [BindProperty]
        public int Number { get; set; }
        public bool Animate { get; set; }

        public IndexModel(IShopDataService ds) : base(ds)
        {
            _ds = ds;
        }

        public void OnGet()
        {
            MainLayout.ShoppingBasket = BasketHelper.GetBasket(HttpContext.Session);
            ViewData["MainLayout"] = MainLayout;


            Highlighted = _ds.GetHighlightedProducts().ToList();
            OnSale = _ds.GetProductsOnSale().ToList();
        }

        public void OnPost()
        {
            Animate = true;
            Product = _ds.GetProductById(ProductId.Value);
            BasketHelper.AddToBasket(HttpContext.Session, ProductId.Value, Product.Price, Number);
            OnGet();
            //MainLayout.ShoppingBasket = BasketHelper.GetBasket(HttpContext.Session);
            //ViewData["MainLayout"] = MainLayout;
        }
    }
}
