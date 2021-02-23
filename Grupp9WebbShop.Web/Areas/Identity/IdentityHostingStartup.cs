using System;
using Grupp9WebbShop.Web.Areas.Identity.Data;
using Grupp9WebbShop.Web.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Grupp9WebbShop.Web.Areas.Identity.IdentityHostingStartup))]
namespace Grupp9WebbShop.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Grupp9WebbShopUserContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Grupp9WebbShopUserContextConnection")));

                services.AddDefaultIdentity<WebbShopUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<Grupp9WebbShopUserContext>();
            });
        }
    }
}