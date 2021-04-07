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
        private readonly ShopContext _context;
        private readonly IShopDataService _ds;

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public CreateModel(ShopContext context, IShopDataService ds)
        {
            _context = context;
            _ds = ds;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name");
            Tags = _ds.GetTagsList();
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public List<SelectListItem> Tags { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (UploadedFile != null)
            {
                if (!System.IO.Directory.Exists("./wwwroot/Images/Uploads")) Directory.CreateDirectory("./wwwroot/Images/Uploads");
                var file = "./wwwroot/Images/Uploads/" + UploadedFile.FileName;
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await UploadedFile.CopyToAsync(fileStream);
                }
                Product.ImageFile = "Uploads/" + UploadedFile.FileName;
            }
            foreach (var tag in _context.Tags)
            {
                var tagName = $"tag-{tag.Id}";
                if (HttpContext.Request.Form[tagName].FirstOrDefault() != null)
                    Product.AllergyTags.Add(tag);
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();
            InventoryItem inventoryRecord = new()
            {
                ProductId = Product.Id,
                Quantity = 0
            };
            _context.Inventory.Add(inventoryRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
