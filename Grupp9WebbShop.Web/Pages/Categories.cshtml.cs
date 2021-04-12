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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grupp9WebbShop.Web.Pages
{
    public class CategoriesModel : BasePageModel
    {
        private readonly IShopDataService _ds;
        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public string CategoryName { get; private set; }
        public IEnumerable<ProductCategory> Categories { get; set; }

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
        public List<SelectListItem> Tags { get; set; }
        public async Task<IActionResult> OnGetAsync(string toggleId = null)
        {
            if (!string.IsNullOrEmpty(toggleId))
            {
                Tags = AllergyTagHelper.LoadTags(HttpContext.Session);
                var tag = Tags.FirstOrDefault(t => t.Value == toggleId);
                tag.Selected = !tag.Selected;
                AllergyTagHelper.SaveTags(HttpContext.Session, Tags);
                return RedirectToPage("./Categories", new { CategoryId = CategoryId });
            }
            Categories = await _ds.GetProductCategoriesAsync();
            Tags = AllergyTagHelper.LoadTags(HttpContext.Session);
            if (Tags == null)
            {
                Tags = await _ds.GetTagsListAsync();
                AllergyTagHelper.SaveTags(HttpContext.Session, Tags);
            }
            if (CategoryId != null)
            {
                Products = await _ds.GetProductsByCategoryAsync(CategoryId.Value);
                CategoryName = Categories.FirstOrDefault(c => c.Id == CategoryId).Name;
            }
            else
            {
                var list = await _ds.GetProductsAsync();
                Products = list.OrderBy(o => o.Name);
            }
            MainLayout.ShoppingBasket = BasketHelper.GetBasket(HttpContext.Session);
            Products = _ds.FilteredProducts(Products, Tags);
            ViewData["MainLayout"] = MainLayout;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Animate = true;
            Product = await _ds.GetProductByIdAsync(ProductId.Value);
            BasketHelper.AddToBasket(HttpContext.Session, ProductId.Value, Product.CalculatedPrice, Number);
            return RedirectToPage("/Categories");
        }
    }
}
