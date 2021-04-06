using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grupp9WebbShop.Data;
using Grupp9WebbShop.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Grupp9WebbShop.Web.Areas.ShopAdmin.Pages
{
//    [Authorize(Policy = "RequireAdministratorRole")]
    public class IndexModel : PageModel
    {
        private readonly IShopDataService _ds;
        public int UnProcessedOrders { get; set; }

        public IndexModel(IShopDataService ds)
        {
            _ds = ds;
        }

        public void OnGet()
        {
            UnProcessedOrders = _ds.GetAllOrders().Where(o => !o.IsProcessed).Count();
        }
    }
}
