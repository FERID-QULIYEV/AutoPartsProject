using Autoparts.Models;

namespace Autoparts.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Slider> Sliders{ get; set; }
        public IEnumerable<IndexCategory> IndexCategories { get; set; }
        public IEnumerable<BestSeller> BestSellers  { get; set; }
        public IEnumerable<NewArrival> NewArrivals { get; set; }
        public IEnumerable<Section> Sections { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<AdvertisingImage> advertisingImages { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<BlogTable> BlogTables{ get; set; }
        public IEnumerable<LastestProduct> LastestProducts { get; set; 
        }
    }
}
