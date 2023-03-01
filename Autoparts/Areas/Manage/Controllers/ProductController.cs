using Autoparts.DAL;
using Autoparts.Models;
using Autoparts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext conntext, IWebHostEnvironment env)
        {
            _context = conntext;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Products.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if (!ModelState.IsValid) return View();
            IFormFile CoverFile = productVM.CoverImage;
            if (!CoverFile.ContentType.Contains("image/"))
            {

                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekil deyil.");
                return View();
            }
            if (!(productVM.CoverImage.Length / 1024 / 1024 < 2))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekilin olcusu 2mb-dan coxdur.");
                return View();
            }
            string CoverFileName = Guid.NewGuid() + CoverFile.FileName;
            using (FileStream stream = new FileStream(Path.Combine(_env.WebRootPath, "image", "catalog", "ProductImage", "CoverImage", CoverFileName), FileMode.Create))
            {
                CoverFile.CopyTo(stream);
            }
            IFormFile InnerFile = productVM.InnerImage;
            if (!InnerFile.ContentType.Contains("image/"))
            {

                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekil deyil.");
                return View();
            }
            if (!(productVM.InnerImage.Length / 1024 / 1024 < 2))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekilin olcusu 2mb-dan coxdur.");
                return View();
            }
            string InnerFileName = Guid.NewGuid() + InnerFile.FileName;
            using (FileStream stream = new FileStream(Path.Combine(_env.WebRootPath, "image", "catalog", "ProductImage", "InnerImage", InnerFileName), FileMode.Create))
            {
                InnerFile.CopyTo(stream);
            }
            IFormFile Otherfile = productVM.OtherImage;
            if (!Otherfile.ContentType.Contains("image/"))
            {

                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekil deyil.");
                return View();
            }
            if (!(productVM.OtherImage.Length / 1024 / 1024 < 2))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekilin olcusu 2mb-dan coxdur.");
                return View();
            }
            string OtherFileName = Guid.NewGuid() + Otherfile.FileName;
            using (FileStream stream = new FileStream(Path.Combine(_env.WebRootPath, "image", "catalog", "ProductImage", "OtherImage", OtherFileName), FileMode.Create))
            {
                Otherfile.CopyTo(stream);
            }
            Product product = new Product { CoverImage = CoverFileName, InnerImage = InnerFileName, OtherImage = OtherFileName, Name = productVM.Name, SellPrice = productVM.SellPrice, DiscountPrice = productVM.DiscountPrice, ColorId = productVM.ColorId, Title = productVM.Title };
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0) return BadRequest();
            Product product = _context.Products.Find(id);
            if (product is null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(int? id, Product product)
        {
                if (id == null || id == 0 || id != product.Id || product is null) return BadRequest();
                if (!ModelState.IsValid) return View();
                Product exist = _context.Products.Find(product.Id);
                exist.CoverImage = product.CoverImage;
                exist.InnerImage = product.InnerImage;
                exist.Name = product.Name;
                exist.SellPrice = product.SellPrice;
                exist.DiscountPrice = product.DiscountPrice;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            Product product= _context.Products.Find(id);
            if (product is null) return NotFound();
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
