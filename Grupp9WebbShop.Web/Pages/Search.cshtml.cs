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
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [BindProperty]
        public int Number { get; set; }
        public List<SelectListItem> Tags { get; set; }

        public SearchModel(IShopDataService ds) : base(ds)
        {
            _ds = ds;
        }
        public IActionResult OnGet(string toggleId = null)
        {
            if (!string.IsNullOrEmpty(toggleId))
            {
                Tags = AllergyTagHelper.LoadTags(HttpContext.Session);
                var tag = Tags.FirstOrDefault(t => t.Value == toggleId);
                tag.Selected = !tag.Selected;
                AllergyTagHelper.SaveTags(HttpContext.Session, Tags);
                return RedirectToPage("./Search", new { Query = Query });
            }

            Tags = AllergyTagHelper.LoadTags(HttpContext.Session);
            if (Tags == null)
            {
                Tags = _ds.GetTagsList();
                AllergyTagHelper.SaveTags(HttpContext.Session, Tags);
            }

            var products = _ds.GetProducts();
            if(!String.IsNullOrEmpty(Query))
            {
            SearchResults = products.Where(p => p.Name.ToUpper().Contains(Query.ToUpper())).ToList();
                if (SearchResults.Count() >0)
                    SearchResults = _ds.FilteredProducts(SearchResults, Tags);
            }

            MainLayout.ShoppingBasket = BasketHelper.GetBasket(HttpContext.Session);
            ViewData["MainLayout"] = MainLayout;


            return Page();
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
