using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grupp9WebbShop.Data;
using Grupp9WebbShop.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Grupp9WebbShop.Web.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly IShopDataService _ds;
        [BindProperty(SupportsGet = true)]
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public bool Animate { get; set; }
        public ProductDetailsModel(IShopDataService ds)
        {
            _ds = ds;
        }

        public void OnGet()
        {
            if (ProductId != null)
            {
                Product = _ds.GetProductById(ProductId.Value);
            }

        }
        public void OnPost()
        {
            Animate = true;
            OnGet();
        }
    }
}
