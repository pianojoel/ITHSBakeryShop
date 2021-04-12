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
        private readonly ShopContext _context;
        private readonly IShopDataService _ds;

        public IFormFile UploadedFile { get; set; }
        public EditModel(ShopContext context, IShopDataService ds)
        {
            _context = context;
            _ds = ds;
        }

        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public List<SelectListItem> Tags { get; set; }

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
            ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name");
            Tags = await _ds.GetTagsListAsync();
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

            var tagsWithProducts = _context.Tags.Include(p => p.Products);
            foreach (var tag in tagsWithProducts)
            {
                var tagName = $"tag-{tag.Id}";
                if (HttpContext.Request.Form[tagName].FirstOrDefault() != null)
                {
                    if (!tag.Products.Contains(Product))
                        Product.AllergyTags.Add(tag);
                }
                else if (tag.Products.Contains(Product)) tag.Products.Remove(Product);
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
