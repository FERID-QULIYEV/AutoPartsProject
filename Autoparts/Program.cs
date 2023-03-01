using Autoparts.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Autoparts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL"));
            });
            builder.Services.AddHttpContextAccessor();
            var app = builder.Build();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllerRoute(name: "areas",pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
            app.MapControllerRoute(name: "Default" , pattern: "{controller=home}/{action=Index}/{id?}");
            app.MapControllerRoute(name: "Category", pattern: "Category", defaults: new { Controller = "Home", Action = "Category" });
            app.MapControllerRoute(name: "BlogPage", pattern: "BlogPage", defaults: new { Controller = "Home", Action = "BlogPage" });
            app.MapControllerRoute(name: "Product", pattern: "Product", defaults: new { Controller = "Home", Action = "Product" });
            app.MapControllerRoute(name: "Cart", pattern: "Cart", defaults: new { Controller = "Home", Action = "Cart" });
            app.MapControllerRoute(name: "Wishlist", pattern: "Wishlist", defaults: new { Controller = "Home", Action = "Wishlist" });
            app.MapControllerRoute(name: "MyAccount", pattern: "MyAccount", defaults: new { Controller = "Home", Action = "MyAccount" });
            app.MapControllerRoute(name: "Login", pattern: "Login", defaults: new { Controller = "Home", Action = "Login" });
            app.MapControllerRoute(name: "Register", pattern: "Register", defaults: new { Controller = "Home", Action = "Register" });

            app.Run();
        }
    }
}