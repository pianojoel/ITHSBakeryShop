using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Grupp9WebbShop.Data;
using Grupp9WebbShop.Data.Models;

namespace Grupp9WebbShop.Web.Areas.ShopAdmin.Pages.CRUD
{
    public class DetailsModel : PageModel
    {
        private readonly Grupp9WebbShop.Data.ShopContext _context;

        public DetailsModel(Grupp9WebbShop.Data.ShopContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products
                .Include(p => p.Category).Include(a => a.AllergyTags).FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
