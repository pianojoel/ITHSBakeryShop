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
    public class IndexModel : PageModel
    {
        private readonly Grupp9WebbShop.Data.ShopContext _context;

        public IndexModel(Grupp9WebbShop.Data.ShopContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Product = await _context.Products
                .Include(p => p.Category).OrderBy(o => o.Name).ToListAsync();
            if (id != null)
            {
                await SetHighlighted(id.Value);
                return RedirectToPage("./Index");
            }
            return Page();
        }
        public async Task SetHighlighted(int pid)
        {
            Product p = await _context.Products.FindAsync(pid);
            p.Highlighted = !p.Highlighted;
            await _context.SaveChangesAsync();
        }
    }
}
