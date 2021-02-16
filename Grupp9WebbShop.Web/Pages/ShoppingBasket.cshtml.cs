using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Grupp9WebbShop.Data.Models;
using Grupp9WebbShop.Data;
using Grupp9WebbShop.Web.Helpers;

namespace Grupp9WebbShop.Web.Pages
{
    public class ShoppingBasketModel : PageModel
    {
        private readonly IShopDataService _ds;

        public IEnumerable<Product> Products { get; set; }
        public ShoppingBasket Basket { get; set; }
        public ShoppingBasketModel(IShopDataService ds)
        {
                        _ds = ds;
        }
        public void OnGet()
        {
            Basket = BasketHelper.GetBasket(HttpContext.Session);
                        Products =  _ds.GetProducts();
        }
        public IActionResult OnGetModifyItem(int pid, bool inc = false, bool delete = false)
        {
            BasketHelper.ModifyItem(HttpContext.Session, pid, inc, delete);
            return RedirectToPage("/shoppingBasket");
        }
    }
}
