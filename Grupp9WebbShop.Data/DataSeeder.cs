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
            prods.Add(CreateProducts(c, "Kanelbulle", 10));
            prods.Add(CreateProducts(c, "Vaniljbulle", 12));
            prods.Add(CreateProducts(c, "Kardemummabulle", 10));
            prods.Add(CreateProducts(c, "Saffransbulle", 15));
            ctx.Products.AddRange(prods);
            ctx.SaveChanges();

        }

        private static Product CreateProducts(ProductCategory c, string name, decimal price, string description = "")
        {
            Product prod = new()
            {
                Name = name,
                Description = description,
                Price = price,
                Category = c
            };
            return prod;
        }

        private static List<ProductCategory> CreateCategories()
        {
            List<ProductCategory> cats = new();
            cats.Add(new ProductCategory()
            {
                Name = "Tårtor"
            });
            cats.Add(new ProductCategory()
            {
                Name = "Bullar"
            });
            cats.Add(new ProductCategory()
            {
                Name = "Småkakor"
            });
            cats.Add(new ProductCategory()
            {
                Name = "Cupcakes"
            });


            return cats;
        }
    }
}
