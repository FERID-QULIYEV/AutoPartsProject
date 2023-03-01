using Autoparts.DAL;
using Autoparts.Models;
using Autoparts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class LastestProductController:Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public LastestProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.LastestProducts.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(LastestProductVM lastestProductVM)
        {
            if (!ModelState.IsValid) return View();
            IFormFile File = lastestProductVM.Image;
            if (!File.ContentType.Contains("image/"))
            {

                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekil deyil.");
                return View();
            }
            if (!(lastestProductVM.Image.Length / 1024 / 1024 < 2))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekilin olcusu 2mb-dan coxdur.");
                return View();
            }
            string FileName = Guid.NewGuid() + File.FileName;
            using (FileStream stream = new FileStream(Path.Combine(_env.WebRootPath, "image", "catalog", "LastestProductImage", FileName), FileMode.Create))
            {
                File.CopyTo(stream);
            }
            if (lastestProductVM.DiscountPrice > 0 && lastestProductVM.DiscountPrice <= 100)
            {
                lastestProductVM.NewSellPrice = ((double)(lastestProductVM.SellPrice * lastestProductVM.DiscountPrice / 100));
                lastestProductVM.NewSellPrice = lastestProductVM.SellPrice - lastestProductVM.NewSellPrice;
            }
            if (lastestProductVM.NewSellPrice < 0 || lastestProductVM.NewSellPrice > lastestProductVM.SellPrice)
            {
                ModelState.AddModelError("DiscountPrice", "Endirim 100%-den cox ve 0% den az olamaz");
                return View();
            }
            LastestProduct lastestProduct = new LastestProduct { Image = FileName,  Name = lastestProductVM.Name, SellPrice = lastestProductVM.SellPrice, DiscountPrice = lastestProductVM.DiscountPrice, NewSellPrice = lastestProductVM.NewSellPrice };
            _context.LastestProducts.Add(lastestProduct);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0) return BadRequest();
            LastestProduct lastestProduct= _context.LastestProducts.Find(id);
            if (lastestProduct is null) return NotFound();
            return View(lastestProduct);
        }
        [HttpPost]
        public IActionResult Update(int? id, LastestProduct lastestProduct)
        {
            if (id == null || id == 0 || id != lastestProduct.Id || lastestProduct is null) return BadRequest();
            if (!ModelState.IsValid) return View();
            if (lastestProduct.DiscountPrice > 0 && lastestProduct.DiscountPrice <= 100)
            {
                lastestProduct.NewSellPrice = ((double)(lastestProduct.SellPrice * lastestProduct.DiscountPrice / 100));
                lastestProduct.NewSellPrice = lastestProduct.SellPrice - lastestProduct.NewSellPrice;
            }
            if (lastestProduct.NewSellPrice < 0 || lastestProduct.NewSellPrice > lastestProduct.SellPrice)
            {
                ModelState.AddModelError("DiscountPrice", "Endirim 100%-den cox ve 0% den az olamaz");
                return View();
            }
            LastestProduct exist = _context.LastestProducts.Find(lastestProduct.Id);
            exist.Image = lastestProduct.Image;
            exist.Name = lastestProduct.Name;
            exist.SellPrice = lastestProduct.SellPrice;
            exist.DiscountPrice = lastestProduct.DiscountPrice;
            exist.NewSellPrice = lastestProduct.NewSellPrice;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            LastestProduct lastestProduct = _context.LastestProducts.Find(id);
            if (lastestProduct is null) return NotFound();
            _context.LastestProducts.Remove(lastestProduct);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
