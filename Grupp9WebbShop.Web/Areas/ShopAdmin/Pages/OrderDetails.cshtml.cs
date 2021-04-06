using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grupp9WebbShop.Data;
using Grupp9WebbShop.Data.Models;
using Grupp9WebbShop.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<WebbShopUser> _userManager;
        public Task<WebbShopUser> User { get; set; }

        public OrderDetailsModel(IShopDataService ds, UserManager<WebbShopUser> userManager)
        {
            _ds = ds;
            _userManager = userManager;
        }
        public void OnGet()
        {
            Order = _ds.GetOrder(OrderId);
            User = _userManager.FindByIdAsync(Order.UserID);
        }
        public IActionResult OnPost()
        {
            _ds.ToggleOrderIsProcessed(OrderId);

            return RedirectToPage("/Orders");
        }
    }
}
