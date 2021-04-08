using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grupp9WebbShop.Data;
using Grupp9WebbShop.Data.Models;
using Grupp9WebbShop.Web.Areas.Identity.Data;
using Grupp9WebbShop.Web.Helpers;
using Grupp9WebbShop.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Grupp9WebbShop.Web.Areas.Identity.Pages.Account
{
    public class UserOrderModel : BasePageModel
    {
        private readonly UserManager<WebbShopUser> _um;
        private readonly IShopDataService _ds;
        public IEnumerable<Order> Orders { get; set; }
        [BindProperty(SupportsGet = true)]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public UserOrderModel(UserManager<WebbShopUser> um, IShopDataService ds) : base(ds)
        {
            _um = um;
            _ds = ds;
        }

        public void OnGet()
        {
            MainLayout.ShoppingBasket = BasketHelper.GetBasket(HttpContext.Session);
            ViewData["MainLayout"] = MainLayout;
            var userID = _um.GetUserId(User);
            Orders = _ds.GetAllOrders().Where(o => o.UserID == userID);
            if(OrderId != 0)
            {
                Order = _ds.GetOrder(OrderId);
            }
        }
        public IActionResult OnPost()
        {
            var userID = _um.GetUserId(User);
            Orders = _ds.GetAllOrders().Where(o => o.UserID == userID);

            if (OrderId != 0)
            {
                Order = _ds.GetOrder(OrderId);

                foreach (var item in Order.OrderItems)
                {
                    BasketHelper.AddToBasket(HttpContext.Session, item.ProductId, item.Price, item.Quantity);
                }
            }
            return RedirectToPage("/ShoppingBasket");
        }
    }
}
