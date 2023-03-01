using Autoparts.DAL;
using Autoparts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Autoparts.Controllers
{
    public class HomeController: Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM home = new HomeVM { Brands = _context.Brands, Sliders = _context.Sliders ,BestSellers =_context.BestSellers,NewArrivals=_context.NewArrivals, IndexCategories=_context.IndexCategories,Blogs=_context.Blogs,Sections=_context.Sections,advertisingImages=_context.AdvertisingImages};
            return View(home);
        }
        public IActionResult Category()
        {
            return View();
        }
        public IActionResult BlogPage()
        {
            HomeVM home = new HomeVM { BlogTables = _context.BlogTables,LastestProducts=_context.LastestProducts};
            return View(home);
        }
        public IActionResult Product()
        {
            HomeVM home = new HomeVM { Products = _context.Products, LastestProducts=_context.LastestProducts};
            return View(home);
        }
        public IActionResult Cart(int? id)
        {
            //List<CartItemVM> items = new List<CartItemVM>();
            //if (!string.IsNullOrEmpty(HttpContext.Request.Cookies["cart"]))
            //{
            //    items = JsonConvert.DeserializeObject<List<CartItemVM>>(HttpContext.Request.Cookies["cart"]);
            //}
            //CartItemVM item = items.FirstOrDefault(i => i.id == id);
            //if (item==null)
            //{
            //    item = new CartItemVM
            //    {
            //        id = (int)id,
            //        Count = 1
            //    };
            //    items.Add(item);
            //}
            //else
            //{
            //    item.Count++;
            //}
            //items.Add(new CartItemVM { id = (int)id, Count = 1 });
            //string Cart = JsonConvert.SerializeObject(items);
            //HttpContext.Response.Cookies.Append("cart", Cart);
            //return Content(Cart);
            return View();
        }
        public IActionResult Wishlist()
        {
            return View();
        }
        public IActionResult Login ()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult MyAccount()
        {
            return View();
        }
    }
}
