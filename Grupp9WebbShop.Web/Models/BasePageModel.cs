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
            LoadProducts();

        }

        private void LoadProducts()
        {
            MainLayout = new LayoutModel()
            {
                ShowNavigation = true,
                Categories = _ds.GetProductCategories(),
                Products = _ds.GetProducts()
            };
        }

        public LayoutModel MainLayout { get; set; }
    }
}
