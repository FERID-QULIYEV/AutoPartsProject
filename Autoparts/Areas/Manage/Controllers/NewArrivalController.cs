using Autoparts.DAL;
using Autoparts.Models;
using Autoparts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class NewArrivalController:Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public NewArrivalController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.NewArrivals.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(NewArrivalVM newArrivalVM)
        {
            if (!ModelState.IsValid) return View();
            IFormFile CoverFile = newArrivalVM.CoverImage;
            if (!CoverFile.ContentType.Contains("image/"))
            {

                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekil deyil.");
                return View();
            }
            if (!(newArrivalVM.CoverImage.Length / 1024 / 1024 < 2))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekilin olcusu 2mb-dan coxdur.");
                return View();
            }
            string CoverFileName = Guid.NewGuid() + CoverFile.FileName;
            using (FileStream stream = new FileStream(Path.Combine(_env.WebRootPath, "image", "catalog", "NewArrivalmage", "CoverImage", CoverFileName), FileMode.Create))
            {
                CoverFile.CopyTo(stream);
            }
            IFormFile InnerFile = newArrivalVM.InnerImage;
            if (!InnerFile.ContentType.Contains("image/"))
            {

                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekil deyil.");
                return View();
            }
            if (!(newArrivalVM.CoverImage.Length / 1024 / 1024 < 2))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekilin olcusu 2mb-dan coxdur.");
                return View();
            }
            string InnerFileName = Guid.NewGuid() + InnerFile.FileName;
            using (FileStream stream = new FileStream(Path.Combine(_env.WebRootPath, "image", "catalog", "NewArrivalmage", "InnerImage", InnerFileName), FileMode.Create))
            {
                InnerFile.CopyTo(stream);
            }
            if (newArrivalVM.DiscountPrice > 0 && newArrivalVM.DiscountPrice <= 100)
            {
                newArrivalVM.NewSellPrice = ((double)(newArrivalVM.SellPrice * newArrivalVM.DiscountPrice / 100));
                newArrivalVM.NewSellPrice = newArrivalVM.SellPrice - newArrivalVM.NewSellPrice;
            }
            if (newArrivalVM.NewSellPrice < 0 || newArrivalVM.NewSellPrice > newArrivalVM.SellPrice)
            {
                ModelState.AddModelError("DiscountPrice", "Endirim 100%-den cox ve 0% den az olamaz");
                return View();
            }
            NewArrival newArrival = new NewArrival { CoverImage = CoverFileName, InnerImage = InnerFileName, Name = newArrivalVM.Name, SellPrice = newArrivalVM.SellPrice, DiscountPrice = newArrivalVM.DiscountPrice, NewSellPrice=newArrivalVM.NewSellPrice};
            _context.NewArrivals.Add(newArrival);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0) return BadRequest();
            NewArrival newArrival= _context.NewArrivals.Find(id);
            if (newArrival is null) return NotFound();
            return View(newArrival);
        }
        [HttpPost]
        public IActionResult Update(int? id, NewArrival newArrival)
        {
            if (id == null || id == 0 || id != newArrival.Id || newArrival is null) return BadRequest();
            if (!ModelState.IsValid) return View();
            if (newArrival.DiscountPrice > 0 && newArrival.DiscountPrice <= 100)
            {
                newArrival.NewSellPrice = ((double)(newArrival.SellPrice * newArrival.DiscountPrice / 100));
                newArrival.NewSellPrice = newArrival.SellPrice - newArrival.NewSellPrice;
            }
            if (newArrival.NewSellPrice < 0 || newArrival.NewSellPrice > newArrival.SellPrice)
            {
                ModelState.AddModelError("DiscountPrice", "Endirim 0% den az ve 100%-den cox olamaz");
                return View();
            }
            NewArrival exist = _context.NewArrivals.Find(newArrival.Id);
            exist.CoverImage = newArrival.CoverImage;
            exist.InnerImage = newArrival.InnerImage;
            exist.Name = newArrival.Name;
            exist.SellPrice = newArrival.SellPrice;
            exist.DiscountPrice = newArrival.DiscountPrice;
            exist.NewSellPrice=newArrival.NewSellPrice;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
            public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            NewArrival newArrival= _context.NewArrivals.Find(id);
            if (newArrival is null) return NotFound();
            _context.NewArrivals.Remove(newArrival);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
