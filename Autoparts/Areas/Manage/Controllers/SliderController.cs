using Autoparts.DAL;
using Autoparts.Models;
using Autoparts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;

namespace Autoparts.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController: Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Sliders.OrderByDescending(s => s.Order));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SliderVM sliderVM)
        {
            if (!ModelState.IsValid) return View();
            IFormFile file= sliderVM.ImageFile;
            if (!file.ContentType.Contains("image/"))
            {

                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekil deyil.");
                return View();
            }
            if (!(sliderVM.ImageFile.Length / 1024 / 1024 < 2))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekilin olcusu 2mb-dan coxdur");
                return View();
            }
            string fileName= Guid.NewGuid()+file.FileName;
            using (var stream = new FileStream(Path.Combine(_env.WebRootPath,"image", "catalog", "slideshow", "ImageSlider", fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Slider slider =new Slider { PrimaryTitle = sliderVM.PrimaryTitle ,SecondaryTitle=sliderVM.SecondaryTitle, Description=sliderVM.Description,Image = fileName,Order=sliderVM.Order};
            if (_context.Sliders.Any(s => s.Order == slider.Order))
            {
                ModelState.AddModelError("Order", $"{slider.Order} sirasinda artiq slider movcuddur");
                return View();
            }
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if(id == null || id==0)return BadRequest();
            Slider slider = _context.Sliders.Find(id);
            if (slider is null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        public IActionResult Update(int? id ,Slider slider)
        {
            if (id == null || id == 0 || id!=slider.Id || slider is null) return BadRequest();
            if (!ModelState.IsValid) return View();
            Slider anotherSlider = _context.Sliders.FirstOrDefault(s => s.Order == slider.Order);
            if (anotherSlider != null)
            {
                anotherSlider.Order = _context.Sliders.Find(id).Order;
            }
            Slider exist = _context.Sliders.Find(slider.Id);
            exist.Order = slider.Order;
            exist.Image=slider.Image;
            exist.PrimaryTitle=slider.PrimaryTitle;
            exist.SecondaryTitle = slider.SecondaryTitle;
            exist.Description = slider.Description;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            Slider slider= _context.Sliders.Find(id);
            if (slider is null) return NotFound();
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
