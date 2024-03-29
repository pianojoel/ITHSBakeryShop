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
        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }
        [BindProperty]
        public IEnumerable<Product> SearchResults { get; set; }
        private readonly IShopDataService _ds;
        [TempData]
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
        public async Task<IActionResult> OnGetAsync(string toggleId = null)
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
                Tags = await _ds.GetTagsListAsync();
                AllergyTagHelper.SaveTags(HttpContext.Session, Tags);
            }

            var products = await _ds.GetProductsAsync();
            if (!String.IsNullOrEmpty(Query))
            {
                SearchResults = products.Where(p => p.Name.ToUpper().Contains(Query.ToUpper()) && p.Category.Name != "F�rdiga paket").ToList();
                if (SearchResults.Count() > 0)
                    SearchResults = _ds.FilteredProducts(SearchResults, Tags);
            }

            MainLayout.ShoppingBasket = BasketHelper.GetBasket(HttpContext.Session);
            ViewData["MainLayout"] = MainLayout;


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Animate = true;
            Product = await _ds.GetProductByIdAsync(ProductId.Value);
            BasketHelper.AddToBasket(HttpContext.Session, ProductId.Value, Product.CalculatedPrice, Number);
            return RedirectToPage("/Search", new { Query = Query });


        }
    }
}
