using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grupp9WebbShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Grupp9WebbShop.Web.Helpers;
using Grupp9WebbShop.Web.Areas.Identity.Data;
using Grupp9WebbShop.Data.Models;

namespace Grupp9WebbShop.Web.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly UserManager<WebbShopUser> _um;
        private readonly IShopDataService _ds;

        public CheckoutModel(UserManager<WebbShopUser> um, IShopDataService ds)
        {
            _um = um;
            _ds = ds;
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public ShippingTypes Shipping { get; set; }
        [BindProperty]
        public PaymentTypes Payment { get; set; }
        public ShoppingBasket Basket { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            Basket = BasketHelper.GetBasket(HttpContext.Session);
            var userId = _um.GetUserId(User);
            Order newOrder = _ds.CreateOrderFromBasket(Basket, userId, Shipping, Payment);
            _ds.SaveOrder(newOrder);
            foreach (var item in Basket.Items)
            {
                _ds.DecreaseProductStock(item.ProductId, item.Quantity);
            }
            return RedirectToPage("/Index");
        }
    }
}
