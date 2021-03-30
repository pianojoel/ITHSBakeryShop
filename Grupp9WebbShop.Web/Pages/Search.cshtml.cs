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
using Grupp9WebbShop.Web.Helpers;

namespace Grupp9WebbShop.Web.Pages
{
    public class SearchModel : BasePageModel
    {
        [BindProperty(SupportsGet =true)]
        public string Query { get; set; }
        [BindProperty]
        public IEnumerable<Product> SearchResults { get; set; }
        private readonly IShopDataService _ds;
        public bool Animate { get; set; }
        public Product Product { get; set; }
        [BindProperty]
        public int? ProductId { get; set; }
        public int Number { get; set; }
       

        public SearchModel(IShopDataService ds) : base(ds)
        {
            _ds = ds;
        }
        public void OnGet()
        {
            var products = _ds.GetProducts();
            if(Query != null)
            {
            SearchResults = products.Where(p => p.Name.ToUpper().Contains(Query.ToUpper())).ToList();
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
