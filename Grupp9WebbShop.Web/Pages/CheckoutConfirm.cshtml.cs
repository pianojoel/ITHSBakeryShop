using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grupp9WebbShop.Data;
using Grupp9WebbShop.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Grupp9WebbShop.Web.Pages
{
    public class CheckoutConfirmModel : PageModel
    {
        private readonly IShopDataService _ds;

        public CheckoutConfirmModel(IShopDataService ds)
        {
            _ds = ds;
        }
        public Order Order { get; set; }
        public void OnGet(int? orderId)
        {
            if (orderId != null)
            {
                Order = _ds.GetOrder(orderId.Value);
            }
        }
    }
}
