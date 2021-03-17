using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grupp9WebbShop.Data;
using Grupp9WebbShop.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Grupp9WebbShop.Web.Areas.ShopAdmin.Pages
{
    public class InventoryAdminModel : PageModel
    {
        private readonly IShopDataService _ds;

        public InventoryAdminModel(IShopDataService ds)
        {
            _ds = ds;
        }
        public IEnumerable<Product> Products { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? EditId { get; set; }
        public IActionResult OnGet(int? id, int? q)
        {
            
            if (id != null)
            {
                _ds.SetProductStock(id.Value, q.Value);
                return RedirectToPage("./InventoryAdmin");
            }
            Products = _ds.GetProducts().OrderBy(o => o.Category.Name).ThenBy(t => t.Name);
            return Page();
        }

    }
}
