using Autoparts.Models;
using Microsoft.EntityFrameworkCore;

namespace Autoparts.DAL
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
                
        }
        public DbSet<AdvertisingImage> AdvertisingImages { get; set; }
        public DbSet<AdvertisingVideo> AdvertisingVideos { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<BestSeller> BestSellers  { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogTable> BlogTables { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<IndexCategory> IndexCategories { get; set; }
        public DbSet<LastestProduct> LastestProducts { get; set; }
        public DbSet<NewArrival> NewArrivals{ get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Slider> Sliders{ get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
