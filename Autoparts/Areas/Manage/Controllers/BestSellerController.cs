using Autoparts.DAL;
using Autoparts.Models;
using Autoparts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Drawing;

namespace Autoparts.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BestSellerController: Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public BestSellerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.BestSellers.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BestSellerVM bestSellerVM)
        {
            if (!ModelState.IsValid) return View();
            IFormFile CoverFile = bestSellerVM.CoverImage;
            if (!CoverFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekil deyil.");
                return View();
            }
            if (!(bestSellerVM.CoverImage.Length / 1024 / 1024 < 2))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekilin olcusu 2mb-dan coxdur.");
                return View();
            }
            string CoverFileName = Guid.NewGuid() + CoverFile.FileName;
            using (FileStream stream = new FileStream(Path.Combine(_env.WebRootPath, "image", "catalog", "BestSellerImage", "CoverImage", CoverFileName), FileMode.Create))
            {
                CoverFile.CopyTo(stream);
            }
            IFormFile InnerFile = bestSellerVM.InnerImage;
            if (!InnerFile.ContentType.Contains("image/"))
            {

                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekil deyil.");
                return View();
            }
            if (!(bestSellerVM.CoverImage.Length / 1024 / 1024 < 2))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekilin olcusu 2mb-dan coxdur.");
                return View();
            }
            string InnerFileName = Guid.NewGuid() + InnerFile.FileName;
            using (FileStream stream = new FileStream(Path.Combine(_env.WebRootPath, "image", "catalog", "BestSellerImage", "InnerImage", InnerFileName), FileMode.Create))
            {
                InnerFile.CopyTo(stream);
            }
            if (bestSellerVM.DiscountPrice > 0 && bestSellerVM.DiscountPrice <= 100)
            {
                bestSellerVM.NewSellPrice = ((double)(bestSellerVM.SellPrice * bestSellerVM.DiscountPrice / 100));
                bestSellerVM.NewSellPrice = bestSellerVM.SellPrice - bestSellerVM.NewSellPrice;
            }
            if (bestSellerVM.NewSellPrice<0 || bestSellerVM.NewSellPrice>bestSellerVM.SellPrice)
            {
                ModelState.AddModelError("DiscountPrice", "Endirim 100%-den cox ve 0% den az olamaz");
                return View();
            }
            BestSeller BestSeller= new BestSeller { CoverImage = CoverFileName, InnerImage = InnerFileName,  Name = bestSellerVM.Name , SellPrice =bestSellerVM.SellPrice ,DiscountPrice=bestSellerVM.DiscountPrice,NewSellPrice=bestSellerVM.NewSellPrice};
            _context.BestSellers.Add(BestSeller);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0) return BadRequest();
            BestSeller bestSeller = _context.BestSellers.Find(id);
            if (bestSeller is null) return NotFound();
            return View(bestSeller);
        }

        [HttpPost]
        public IActionResult Update(int? id, BestSeller bestSeller)
        {
            if (id == null || id == 0 || id != bestSeller.Id || bestSeller is null) return BadRequest();
            if (!ModelState.IsValid) return View();
            BestSeller exist = _context.BestSellers.Find(bestSeller.Id);
            if (bestSeller.DiscountPrice > 0 && bestSeller.DiscountPrice <= 100)
            {
                bestSeller.NewSellPrice = ((double)(bestSeller.SellPrice * bestSeller.DiscountPrice / 100));
                bestSeller.NewSellPrice = bestSeller.SellPrice - bestSeller.NewSellPrice;
            }
            if (bestSeller.NewSellPrice < 0 || bestSeller.NewSellPrice > bestSeller.SellPrice)
            {
                ModelState.AddModelError("DiscountPrice", "Endirim 100%-den cox ve 0% den az olamaz");
                return View();
            }
            exist.CoverImage = bestSeller.CoverImage;
            exist.InnerImage = bestSeller.InnerImage;
            exist.Name = bestSeller.Name;
            exist.SellPrice = bestSeller.SellPrice;
            exist.DiscountPrice = bestSeller.DiscountPrice;
            exist.NewSellPrice = bestSeller.NewSellPrice;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            BestSeller bestSeller = _context.BestSellers.Find(id);
            if (bestSeller is null) return NotFound();
            _context.BestSellers.Remove(bestSeller);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
