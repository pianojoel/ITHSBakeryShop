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
    public class OrderDetailsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        private readonly IShopDataService _ds;

        public OrderDetailsModel(IShopDataService ds)
        {
            _ds = ds;
        }
        public void OnGet()
        {
            Order = _ds.GetOrder(OrderId);
        }
        public IActionResult OnPost()
        {
            _ds.ToggleOrderIsProcessed(OrderId);

            return RedirectToPage("/Orders");
        }
    }
}
