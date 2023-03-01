using Microsoft.AspNetCore.Mvc;
using Autoparts.DAL;
using Autoparts.Models;
using Autoparts.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Autoparts.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BrandController: Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public BrandController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Brands.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BrandVM brandvm)
        {
            if (!ModelState.IsValid) return View();
            IFormFile file=brandvm.ImageFile;
            if (!file.ContentType.Contains("image/"))
            {

                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekil deyil.");
                return View();
            }
            if (!(brandvm.ImageFile.Length / 1024 / 1024 < 2))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekilin olcusu 2mb-dan coxdur.");
                return View();
            }
            string filename= Guid.NewGuid()+file.FileName;
            using (FileStream stream = new FileStream(Path.Combine(_env.WebRootPath, "image", "catalog","brands","AddImage", filename), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Brand brand = new Brand { Image = filename, Name=brandvm.Name, Link=brandvm.Link};
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0) return BadRequest();
            Brand brand = _context.Brands.Find(id);
            if (brand is null) return NotFound();
            return View(brand);
        }
        [HttpPost]
        public IActionResult Update(int? id, Brand brand)
        {
            if (id == null || id == 0 || id != brand.Id || brand is null) return BadRequest();
            if (!ModelState.IsValid) return View();
            Brand exist = _context.Brands.Find(brand.Id);
            exist.Image = brand.Image;
            exist.Name = brand.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            Brand brand = _context.Brands.Find(id);
            if (brand is null) return NotFound();
            _context.Brands.Remove(brand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
