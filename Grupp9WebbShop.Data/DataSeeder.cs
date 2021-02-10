using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Grupp9WebbShop.Data.Models;
namespace Grupp9WebbShop.Data
{
    public class DataSeeder
    {
        public static void SeedDatabase(ShopContext ctx)
        {
            List<ProductCategory> cats = CreateCategories();
            ctx.ProductCategories.AddRange(cats);
            ctx.SaveChanges();
            var c = ctx.ProductCategories.Where(n => n.Name == "Bullar").FirstOrDefault();
            List<Product> prods = new();
            prods.Add(CreateProducts(c, "Kanelbulle", 10, "Kanelbullen har sitt ursprung från Sverige. Man tror att bakverket kom till på 1920 talet.", "cinnamon-buns1.jpg", "En bild på en kanelbulle", "Kanelbullen har sitt ursprung från Sverige."));
            prods.Add(CreateProducts(c, "Vaniljbulle", 12, "Solskensbullar, vaniljbullar, seglarbullar, sockerbullar… kärt bakverk har många namn!", "vanilla-bun.jpg", "En bild på en vaniljbulle", "Solskensbullar, vaniljbullar, seglarbullar, sockerbullar…"));
            prods.Add(CreateProducts(c, "Kardemummabulle", 10, "Den söta, saftiga varianten på kanelbullen.", "cardemum-bun.jpg", "En bild på en kardemummabulle", "Den söta, saftiga varianten på kanelbullen."));
            prods.Add(CreateProducts(c, "Saffransbulle", 15, "Saffransbullar är en av julens absoluta höjdpunkter. Saffransbullarna kan göras på många olika sätt, men vi vågar säga att detta är absolut godaste!", "safferon-bun.jpg", "En bild på en saffransbulle", "En av julens absoluta höjdpunkter"));
            prods.Add(CreateProducts(c, "Toscabulle", 17, "Vetebulle med mandelfyllning och krispig söt toscatopping","tosca-bun.jpg", "En bild på en toscabulle", "Vetebulle med mandelfyllning och krispig söt toscatopping"));
            prods.Add(CreateProducts(c, "Kanellängd", 30, "Klassisk kanellängd toppad med pärlsocker.", "cinnamon-roll.jpg", "En bild på en kanellängd", "Klassisk kanellängd toppad med pärlsocker."));
            c = ctx.ProductCategories.Where(n => n.Name == "Tårtor").FirstOrDefault();

            ctx.Products.AddRange(prods);
            ctx.SaveChanges();

        }

        private static Product CreateProducts(ProductCategory c, string name, decimal price, string description = "", string imageFile = "", string imageDesc = "", string summary = "")
        {
            Product prod = new()
            {
                Name = name,
                Description = description,
                Price = price,
                Category = c,
                ImageFile = imageFile,
                ImageDescription = imageDesc,
                Summary = summary
            };
            return prod;
        }

        private static List<ProductCategory> CreateCategories()
        {
            List<ProductCategory> cats = new();
            cats.Add(new ProductCategory()
            {
                Name = "Tårtor",
                Description = "Våra tårtor bakas med organiska varor. Lokalproducerat och ekologiskt. Fairtrade certifierat.",
                ImageFile ="tartor.jpeg",
                ImageDescription="En bild på en tårta"
            });
            cats.Add(new ProductCategory()
            {
                Name = "Bullar",
                Description ="Bullar direkt ur ugnen!",
                ImageFile = "kaffebullar.jpg",
                ImageDescription = "En bild på bullar"
            });
            cats.Add(new ProductCategory()
            {
                Name = "Småkakor",
                Description= "Hos oss hittar ni goda, festliga småkakor som bara vill hoppa in i munnen. Passar både till fikat eller kalaset!",
                ImageFile = "smakakor.jpg",
                ImageDescription = "En bild på småkakor"
            });
            cats.Add(new ProductCategory()
            {
                Name = "Cupcakes",
                Description= "Här hittar ni fantastiska smarriga cupcakes med färgglada frostings. Kan förgylla er dag!",
                ImageFile = "cupcake.jpeg",
                ImageDescription = "En bild på cupcakes"
            });


            return cats;
        }
    }
}
