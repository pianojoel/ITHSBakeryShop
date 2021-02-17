using Grupp9WebbShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grupp9WebbShop.Data;
using Grupp9WebbShop.Web.Helpers;

namespace Grupp9WebbShop.Web.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly IShopDataService _ds;

        public IndexModel(IShopDataService ds) : base(ds)
        {
            _ds = ds;
        }

        public void OnGet()
        {
            MainLayout.ShoppingBasket = BasketHelper.GetBasket(HttpContext.Session);
            ViewData["MainLayout"] = MainLayout;
        }
    }
}
