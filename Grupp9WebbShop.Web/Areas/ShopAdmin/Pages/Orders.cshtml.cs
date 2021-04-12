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
    public class OrdersModel : PageModel
    {
        private readonly IShopDataService _ds;
        public IEnumerable<Order> Orders { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool ShowNotProcessedOrders { get; set; }

        public OrdersModel(IShopDataService ds) 
        {
            _ds = ds;
        }

        public async Task OnGet()
        {
            Orders = await _ds.GetAllOrdersAsync();
            if (ShowNotProcessedOrders)
            {
                Orders = Orders.Where(o => !o.IsProcessed);
            }

        }
    }
}
