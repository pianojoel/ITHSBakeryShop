using Grupp9WebbShop.Data;
using Grupp9WebbShop.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grupp9WebbShop.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnSaleController : ControllerBase
    {
        private readonly IShopDataService _ds;

        public OnSaleController(IShopDataService ds)
        {
            _ds = ds;
        }
        [HttpGet]
        public IEnumerable<Product> get()
        {
            return _ds.GetProductsOnSale();
        }
    }
}
