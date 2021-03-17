using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grupp9WebbShop.Data;
using Grupp9WebbShop.Data.Models;
using Grupp9WebbShop.Web.Helpers;
using Grupp9WebbShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Grupp9WebbShop.Web.Pages
{
    public class ProductDetailsModel : BasePageModel
    {
        private readonly IShopDataService _ds;
        [BindProperty(SupportsGet = true)]
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public int ProductQuantity { get; set; }
        public bool Animate { get; set; }
        public ProductDetailsModel(IShopDataService ds) : base(ds)
        {
            _ds = ds;
        }

        public void OnGet()
        {
            if (ProductId != null)
            {
                Product = _ds.GetProductById(ProductId.Value);
                ProductQuantity = _ds.GetProductStock(ProductId.Value);
            }
            MainLayout.ShoppingBasket = BasketHelper.GetBasket(HttpContext.Session);
            ViewData["MainLayout"] = MainLayout;
        }

        [BindProperty]
        public int Number { get; set; }
       
        public void OnPost()
        {
            Animate = true;
            Product = _ds.GetProductById(ProductId.Value);
            BasketHelper.AddToBasket(HttpContext.Session, ProductId.Value, Product.Price, Number);
            OnGet();
            MainLayout.ShoppingBasket = BasketHelper.GetBasket(HttpContext.Session);
            ViewData["MainLayout"] = MainLayout;
        }
    }
}
