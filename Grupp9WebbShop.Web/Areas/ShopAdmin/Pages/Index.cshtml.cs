using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Grupp9WebbShop.Web.Areas.ShopAdmin.Pages
{
//    [Authorize(Policy = "RequireAdministratorRole")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
