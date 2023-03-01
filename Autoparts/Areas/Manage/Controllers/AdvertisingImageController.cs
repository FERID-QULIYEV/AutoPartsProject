using Autoparts.DAL;
using Autoparts.Models;
using Autoparts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AdvertisingImageController: Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public AdvertisingImageController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.AdvertisingImages.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AdvertisingImageVM advertisingImageVM)
        {
            if (!ModelState.IsValid) return View();
            IFormFile formFile = advertisingImageVM.Image;
            if (advertisingImageVM.ImageCount == 0)
            {
                ModelState.AddModelError("count", "Sizin artiq sekille reklaminiz var.");
                return View();
            }
            if (!formFile.ContentType.Contains("image/"))
            {

                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekil deyil.");
                return View();
            }
            if (!(advertisingImageVM.Image.Length / 1024 / 1024 < 2))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekilin olcusu 2mb-dan coxdur.");
                return View();
            }
            string FileName = Guid.NewGuid() + formFile.FileName;
            using (FileStream stream = new FileStream(Path.Combine(_env.WebRootPath, "image", "catalog", "Advertisings", "Image", FileName), FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            AdvertisingImage advertisingImage = new AdvertisingImage { Image =FileName,Count=advertisingImageVM.ImageCount};
            if (advertisingImageVM.ImageCount > 0)
            {
                advertisingImageVM.ImageCount = advertisingImageVM.ImageCount - 1;
            }
            _context.AdvertisingImages.Add(advertisingImage);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            AdvertisingImage advertisingImage = _context.AdvertisingImages.Find(id);
            if (advertisingImage is null) return NotFound();
            _context.AdvertisingImages.Remove(advertisingImage);
            advertisingImage.Count = advertisingImage.Count + 1;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
