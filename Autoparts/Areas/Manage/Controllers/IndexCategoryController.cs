using Autoparts.DAL;
using Autoparts.Models;
using Autoparts.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace Autoparts.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class IndexCategoryController:Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public IndexCategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.IndexCategories.OrderByDescending(i =>i.SectionId));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(IndexCategoryVM indexCategoryVM)
        {
            if (!ModelState.IsValid) return View();
            IFormFile file = indexCategoryVM.Image;
            if (!file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekil deyil.");
                return View();
            }
            if (!(indexCategoryVM.Image.Length / 1024 / 1024 < 2))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekilin olcusu 2mb-dan coxdur.");
                return View();
            }
            string filename = Guid.NewGuid() + file.FileName;
            using (FileStream stream = new FileStream(Path.Combine(_env.WebRootPath, "image", "catalog", "IndexCategory", filename), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            IndexCategory indexCategory = new IndexCategory { Image = filename, Name = indexCategoryVM.Name , SectionId =indexCategoryVM.SectionId};
            //if (indexCategory.Section.Id!=indexCategory.SectionId)
            //{
            //    ModelState.AddModelError("Order", $"{indexCategory.SectionId} Id yoxdu ");
            //    return View();
            //}
            _context.IndexCategories.Add(indexCategory);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            IndexCategory indexCategory = _context.IndexCategories.Find(id);
            if (indexCategory is null) return NotFound();
            _context.IndexCategories.Remove(indexCategory);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0) return BadRequest();
            IndexCategory indexCategory = _context.IndexCategories.Find(id);
            if (indexCategory is null) return NotFound();
            return View(indexCategory);
        }
        [HttpPost]
        public IActionResult Update(int? id, IndexCategory indexCategory)
        {
            if (id == null || id == 0 || id != indexCategory.Id || indexCategory is null) return BadRequest();
            if (!ModelState.IsValid) return View();
            IndexCategory exist = _context.IndexCategories.Find(indexCategory.Id);
            exist.Image = indexCategory.Image;
            exist.Name = indexCategory.Name;
            exist.SectionId = indexCategory.SectionId;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult NewSectionIndex()
        {
            return View(_context.Sections.ToList());
        }

        public IActionResult NewSection()
        {
            return View();
        }
        [HttpPost]

        public IActionResult NewSection(SectionVM sectionVM)
        {
            if (!ModelState.IsValid) return View();
            Section section = new Section { Section1 = sectionVM.Section1, Section2 = sectionVM?.Section2, Section3 = sectionVM?.Section3, Section4 = sectionVM?.Section4, };
            _context.Sections.Add(section);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult NewSectionDelete(int? id)
        {
            if (id is null) return BadRequest();
            Section section = _context.Sections.Find(id);
            if (section is null) return NotFound();
            _context.Sections.Remove(section);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
