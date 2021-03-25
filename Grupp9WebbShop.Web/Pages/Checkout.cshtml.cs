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
        public async Task<IActionResult> OnPostAsync()
        {

            return RedirectToPage("/");
        }
    }
}
