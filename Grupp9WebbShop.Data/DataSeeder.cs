﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Grupp9WebbShop.Data.Models;
using System.IO;

namespace Grupp9WebbShop.Data
{
    public class DataSeeder
    {
        private static string fileName = "DataImport.csv";
        private static string catsFileName = "Categories.csv";
        private static Random rnd = new();
        public static void SeedDatabaseFromCsv(ShopContext ctx)
        {
            List<Product> prods = new();

            List<ProductCategory> cats = CreateCategoriesFromFile();
            ctx.ProductCategories.AddRange(cats);
            ctx.SaveChanges();

            var data = File.ReadLines(fileName);
            foreach (var line in data)
            {
                string[] chunk = line.Split(';');
                var c = ctx.ProductCategories.Where(n => n.Name == chunk[0]).FirstOrDefault();
                prods.Add(CreateProducts(ctx, c, chunk[1], decimal.Parse(chunk[2]), chunk[3], chunk[4], chunk[5], chunk[6]));
                //ctx.Products.Add(CreateProducts(c, chunk[1], decimal.Parse(chunk[2]), chunk[3], chunk[4], chunk[5]));
                //ctx.SaveChanges();
            }
            ctx.Products.AddRange(prods);
            ctx.SaveChanges();
            var highlightList = ctx.Products.Take(3);
            foreach (var p in highlightList)
            {
                p.Highlighted = true;
            }
            ctx.SaveChanges();
        }
        //public static void SeedDatabase(ShopContext ctx)
        //{
        //    List<ProductCategory> cats = CreateCategories();
        //    ctx.ProductCategories.AddRange(cats);
        //    ctx.SaveChanges();
        //    var c = ctx.ProductCategories.Where(n => n.Name == "Bullar").FirstOrDefault();
        //    List<Product> prods = new();
        //    prods.Add(CreateProducts(c, "Kanelbulle", 20, "Kanelbullen har sitt ursprung från Sverige. Man tror att bakverket kom till på 1920 talet.", "cinnamon-buns1.jpg", "En bild på en kanelbulle", "Kanelbullen har sitt ursprung från Sverige."));
        //    prods.Add(CreateProducts(c, "Vaniljbulle", 20, "Solskensbullar, vaniljbullar, seglarbullar, sockerbullar… kärt bakverk har många namn!", "vanilla-bun.jpg", "En bild på en vaniljbulle", "Solskensbullar, vaniljbullar, seglarbullar, sockerbullar…"));
        //    prods.Add(CreateProducts(c, "Kardemummabulle", 20, "Den söta, saftiga varianten på kanelbullen.", "cardemum-bun.jpg", "En bild på en kardemummabulle", "Den söta, saftiga varianten på kanelbullen."));
        //    prods.Add(CreateProducts(c, "Saffransbulle", 25, "Saffransbullar är en av julens absoluta höjdpunkter. Saffransbullarna kan göras på många olika sätt, men vi vågar säga att detta är absolut godaste!", "safferon-bun.jpg", "En bild på en saffransbulle", "En av julens absoluta höjdpunkter"));
        //    prods.Add(CreateProducts(c, "Toscabulle", 20, "Vetebulle med mandelfyllning och krispig söt toscatopping", "tosca-bun.jpg", "En bild på en toscabulle", "Vetebulle med mandelfyllning och krispig söt toscatopping"));
        //    prods.Add(CreateProducts(c, "Kanellängd", 40, "Klassisk kanellängd toppad med pärlsocker.", "cinnamon-roll.jpg", "En bild på en kanellängd", "Klassisk kanellängd toppad med pärlsocker."));
        //    c = ctx.ProductCategories.Where(n => n.Name == "Tårtor").FirstOrDefault();

        //    ctx.Products.AddRange(prods);
        //    ctx.SaveChanges();

        //}

        private static Product CreateProducts(ShopContext ctx, ProductCategory cat, string name, decimal price, string allergyInfo = "", string imageFile = "", string imageDesc = "", string description = "", string summary = "")
        {
            Product prod = new()
            {
                Name = name,
                Description = description,
                //                AllergyInfo = allergyInfo,
                Price = price,
                Category = cat,
                ImageFile = imageFile,
                ImageDescription = imageDesc,
                Summary = summary,
                AddedDate = CreateRandomDate()
            };
            string[] allergyChunks = allergyInfo.Split(',');
            foreach (var c in allergyChunks)
            {
                var tag = ctx.Tags.Where(n => n.Name == c).FirstOrDefault();
                if (tag == null && !string.IsNullOrEmpty(c))
                {
                    tag = new()
                    {
                        Name = c
                    };
                    ctx.Tags.Add(tag);
                    ctx.SaveChanges();
                }
                if (tag != null)
                    prod.AllergyTags.Add(tag);
            }
            return prod;
        }

        private static List<ProductCategory> CreateCategoriesFromFile()
        {
            List<ProductCategory> cats = new();
            var data = File.ReadLines(catsFileName);
            foreach (var line in data)
            {
                string[] chunk = line.Split(';');
                cats.Add(new ProductCategory()
                {
                    Name = chunk[1],
                    DisplayOrder = int.Parse(chunk[0])
                });

            }
            return cats;
        }
        //private static List<ProductCategory> CreateCategories()
        //{
        //    List<ProductCategory> cats = new();
        //    cats.Add(new ProductCategory()
        //    {
        //        Name = "Tårtor",
        //        DisplayOrder = 4
        //    });
        //    cats.Add(new ProductCategory()
        //    {
        //        Name = "Bullar",
        //        DisplayOrder = 1
        //    });
        //    cats.Add(new ProductCategory()
        //    {
        //        Name = "Småkakor",
        //        DisplayOrder = 3
        //    });
        //    cats.Add(new ProductCategory()
        //    {
        //        Name = "Cupcakes",
        //        DisplayOrder = 2
        //    });
        //    cats.Add(new ProductCategory()
        //    {
        //        Name = "Färdiga paket",
        //        DisplayOrder = 5
        //    });


        //    return cats;
        //}
        private static DateTime CreateRandomDate()
        {
            DateTime startDate = DateTime.Now - TimeSpan.FromDays(30);
            var randomDay = rnd.Next(1, 31);
            return startDate + TimeSpan.FromDays(randomDay);
        }
    }
}
