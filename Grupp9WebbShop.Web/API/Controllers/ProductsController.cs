using Grupp9WebbShop.Data;
using Grupp9WebbShop.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Grupp9WebbShop.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IShopDataService _ds;

        public ProductsController(IShopDataService ds)
        {
            _ds = ds;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAsync()
        {
            
            return await _ds.GetProductsAsync();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetAsync(int id)
        {
            var prod = await _ds.GetProductByIdAsync(id);
            if (prod == null)
                return NotFound();
            return prod;
        }

    }
}
