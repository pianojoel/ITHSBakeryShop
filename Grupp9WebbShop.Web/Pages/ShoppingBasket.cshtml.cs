using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Grupp9WebbShop.Data.Models;
using Grupp9WebbShop.Data;
using Grupp9WebbShop.Web.Helpers;
using Grupp9WebbShop.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace Grupp9WebbShop.Web.Pages
{
    public class ShoppingBasketModel : BasePageModel
    {
        private readonly IShopDataService _ds;

        public IEnumerable<Product> Products { get; set; }
        public ShoppingBasket Basket { get; set; }
        public ShoppingBasketModel(IShopDataService ds) : base(ds)
        {
            _ds = ds;
        }
        public void OnGet()
        {
            Basket = BasketHelper.GetBasket(HttpContext.Session);
            Products = _ds.GetProducts();
            MainLayout.ShoppingBasket = Basket;
            ViewData["MainLayout"] = MainLayout;
        }
        public IActionResult OnGetModifyItem(int pid, bool inc = false, bool delete = false)
        {
            BasketHelper.ModifyItem(HttpContext.Session, pid, inc, delete);
            return RedirectToPage("/shoppingBasket");
        }


        [Required(ErrorMessage = "En eller flera av produkterna du försökte köpa finns ej i lager. Din varukorg har uppdaterats.")]
        public bool? StockStatusOK { get; set; } = true;

        public IActionResult OnGetCheckStock()
        {
            Basket = BasketHelper.GetBasket(HttpContext.Session);

            foreach (var item in Basket.Items)
            {
                int stockBalance = _ds.GetProductStock(item.ProductId);

                if (item.Quantity > stockBalance)
                {
                    StockStatusOK = null;

                    int length = item.Quantity - stockBalance;

                    for (int i = 0; i < length; i++)
                    {
                        BasketHelper.ModifyItem(HttpContext.Session, item.ProductId, false, false);
                    }
                }
            }

            if (StockStatusOK == true)
            { 
                return RedirectToPage("/Checkout");
            }
            Basket = BasketHelper.GetBasket(HttpContext.Session);
            return  RedirectToPage("/shoppingBasket"); 
        }
    }
}
