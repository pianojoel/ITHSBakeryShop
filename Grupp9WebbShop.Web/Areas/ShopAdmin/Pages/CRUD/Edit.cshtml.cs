using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Grupp9WebbShop.Data;
using Grupp9WebbShop.Data.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Grupp9WebbShop.Web.Areas.ShopAdmin.Pages.CRUD
{
    public class EditModel : PageModel
    {
        private readonly Grupp9WebbShop.Data.ShopContext _context;
        public IFormFile UploadedFile { get; set; }
        public EditModel(Grupp9WebbShop.Data.ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products
                .Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Product).State = EntityState.Modified;

            if (UploadedFile != null)
            {
                var file = "./wwwroot/Images/Uploads/" + UploadedFile.FileName;
                var deleteFile = "./wwwroot/images/" + Product.ImageFile;
                if (deleteFile.Contains("Uploads/"))
                    System.IO.File.Delete(deleteFile);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await UploadedFile.CopyToAsync(fileStream);
                }

                Product.ImageFile = "Uploads/" + UploadedFile.FileName;
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
