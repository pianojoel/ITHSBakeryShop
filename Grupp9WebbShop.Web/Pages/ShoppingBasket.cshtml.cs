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
        public async Task OnGetAsync()
        {
            Basket = BasketHelper.GetBasket(HttpContext.Session);
            Products = await _ds.GetProductsAsync();
            MainLayout.ShoppingBasket = Basket;
            ViewData["MainLayout"] = MainLayout;
        }
        public IActionResult OnGetModifyItem(int pid, bool inc = false, bool delete = false)
        {
            BasketHelper.ModifyItem(HttpContext.Session, pid, inc, delete);
            return RedirectToPage("/shoppingBasket");
        }

        [BindProperty(SupportsGet = true)]
        public bool StockStatusUpdated { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool StockStatusOK { get; set; } = true;

        [BindProperty(SupportsGet = true)]
        public bool IsNotEmpty { get; set; } = true;


        public async Task<IActionResult> OnGetCheckStockAsync()
        {
            Basket = BasketHelper.GetBasket(HttpContext.Session);

            if(Basket.Items.Count == 0)
            {
                IsNotEmpty = false;
                return Page();
            }
            else
            {
                IsNotEmpty = true;
            }

            Basket = BasketHelper.GetBasket(HttpContext.Session);

            foreach (var item in Basket.Items)
            {
                int stockBalance = _ds.GetProductStock(item.ProductId);

                if (item.Quantity > stockBalance)
                {
                    int length = item.Quantity - stockBalance;

                    for (int i = 0; i < length; i++)
                    {
                        BasketHelper.ModifyItem(HttpContext.Session, item.ProductId, false, false);
                    }
                    StockStatusOK = false;
                StockStatusUpdated = true;
                }
            }

            if (StockStatusOK)
            { 
                return RedirectToPage("/Checkout");
            }

            Basket = BasketHelper.GetBasket(HttpContext.Session);
            Products = await _ds.GetProductsAsync();
            MainLayout.ShoppingBasket = Basket;
            return Page(); 
        }
    }
}
