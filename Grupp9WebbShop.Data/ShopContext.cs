using System;
using System.Collections.Generic;
using System.Text;
using Grupp9WebbShop.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace Grupp9WebbShop.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
