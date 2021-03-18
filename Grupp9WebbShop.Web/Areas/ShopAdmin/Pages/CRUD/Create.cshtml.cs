using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Grupp9WebbShop.Data;
using Grupp9WebbShop.Data.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Grupp9WebbShop.Web.Areas.ShopAdmin.Pages.CRUD
{
    public class CreateModel : PageModel
    {
        private readonly Grupp9WebbShop.Data.ShopContext _context;
        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public CreateModel(Grupp9WebbShop.Data.ShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (UploadedFile != null)
            {
                var file = "./wwwroot/Images/Uploads/" + UploadedFile.FileName;
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await UploadedFile.CopyToAsync(fileStream);
                }
                Product.ImageFile = "Uploads/" + UploadedFile.FileName;
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
