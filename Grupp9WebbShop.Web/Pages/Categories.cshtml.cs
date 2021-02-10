using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grupp9WebbShop.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Grupp9WebbShop.Data;
namespace Grupp9WebbShop.Web.Pages
{
    public class CategoriesModel : PageModel
    {
        private readonly IShopDataService _ds;
        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }
        public IEnumerable<Product> Products {get; set;}
        public IEnumerable<ProductCategory> Categories { get; set;  }
        public CategoriesModel(IShopDataService ds)
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

        }
    }
}
