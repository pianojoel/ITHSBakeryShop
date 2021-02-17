using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grupp9WebbShop.Data;
namespace Grupp9WebbShop.Web.Models
{
    public class BasePageModel : PageModel
    {
        private readonly IShopDataService _ds;

        public BasePageModel(IShopDataService ds) : base()
        {
            _ds = ds;
            MainLayout = new LayoutModel()
            {
                ShowNavigation = true,
                Categories = _ds.GetProductCategories()
            };
            
        }
        public LayoutModel MainLayout { get; set; }
    }
}
