using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grupp9WebbShop.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Grupp9WebbShop.Data;
using Grupp9WebbShop.Web.Models;
using Grupp9WebbShop.Web.Helpers;

namespace Grupp9WebbShop.Web.Pages
{
    public class CategoriesModel : BasePageModel
    {
        private readonly IShopDataService _ds;
        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }
        public IEnumerable<Product> Products {get; set;}
        public IEnumerable<ProductCategory> Categories { get; set;  }

        [BindProperty]
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        [BindProperty]
        public int Number { get; set; }
        public bool Animate { get; set; }

        public CategoriesModel(IShopDataService ds) : base(ds)
        {
                        _ds = ds;
            
        }
        public void OnGet()
        {
            Categories = _ds.GetProductCategories();
            if (CategoryId !=  null)
            {
                Products = _ds.GetProductsByCategory(CategoryId.Value);
            }
            MainLayout.ShoppingBasket = BasketHelper.GetBasket(HttpContext.Session);
            ViewData["MainLayout"] = MainLayout;
        }

        public void OnPost()
        {
            Animate = true;
            Product = _ds.GetProductById(ProductId.Value);
            BasketHelper.AddToBasket(HttpContext.Session, ProductId.Value, Product.CalculatedPrice, Number);
            OnGet();
        }
    }
}
