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
using System.ComponentModel.DataAnnotations;
using Grupp9WebbShop.Web.Models;

namespace Grupp9WebbShop.Web.Pages
{
    public class CheckoutModel : BasePageModel
    {
        private readonly UserManager<WebbShopUser> _um;
        private readonly IShopDataService _ds;

        public CheckoutModel(UserManager<WebbShopUser> um, IShopDataService ds) : base(ds)
        {
            _um = um;
            _ds = ds;
        }
        public void OnGet()
        {
            Basket = BasketHelper.GetBasket(HttpContext.Session);
            MainLayout.ShoppingBasket = BasketHelper.GetBasket(HttpContext.Session);
            ViewData["MainLayout"] = MainLayout;

        }
        [Required(ErrorMessage = "Du måste välja en leveransmetod")]
        [BindProperty]
        public ShippingTypes? Shipping { get; set; }
        [Required(ErrorMessage = "Du måste välja en betalningsmetod")]
        [BindProperty]
        public PaymentTypes? Payment { get; set; }
        public ShoppingBasket Basket { get; set; }
[BindProperty]
        public bool? Fail { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Basket = BasketHelper.GetBasket(HttpContext.Session);
                Fail = true;
                return Page();
            }
            Basket = BasketHelper.GetBasket(HttpContext.Session);
            var userId = _um.GetUserId(User);
            Order newOrder = _ds.CreateOrderFromBasket(Basket, userId, Shipping.Value, Payment.Value);
            _ds.SaveOrder(newOrder);
            foreach (var item in Basket.Items)
            {
                _ds.DecreaseProductStock(item.ProductId, item.Quantity);
            }
            BasketHelper.ClearBasket(HttpContext.Session);
            return RedirectToPage("/Index");
        }
    }
}
