using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Grupp9WebbShop.Data;
using Grupp9WebbShop.Web.Areas.Identity.Data;
using Grupp9WebbShop.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Grupp9WebbShop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            // Initialize the database
            var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ShopContext>();
                var udb = scope.ServiceProvider.GetRequiredService<Grupp9WebbShopUserContext>();
                var ds = scope.ServiceProvider.GetRequiredService<IShopDataService>();
                // Uncoment if you want to delete an existing database and start over.
                // db.Database.EnsureDeleted();
                if (db.Database.EnsureCreated())
                {
                    DataSeeder.SeedDatabaseFromCsv(db);
                    udb.Database.Migrate();
                    foreach (var prod in ds.GetProducts())
                    {
                        ds.SetProductStock(prod.Id, 10);
                    }
                }
            }


            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
