using Autoparts.DAL;
using Autoparts.Models;
using Autoparts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BlogPageController: Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public BlogPageController(AppDbContext conntext, IWebHostEnvironment env)
        {
            _context = conntext;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.BlogTables.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogTableVM blogTableVM)
        {
            
            IFormFile file = blogTableVM.Image;
            if (!file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekil deyil.");
                return View();
            }
            if (!(blogTableVM.Image.Length / 1024 / 1024 < 2))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekilin olcusu 2mb-dan coxdur.");
                return View();
            }
            string ImageFile = Guid.NewGuid() + file.FileName;
            using (FileStream stream = new FileStream(Path.Combine(_env.WebRootPath, "image", "catalog", "blog", "NewBlogPageImage", ImageFile), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            if (blogTableVM.Date.Month==1)
            {
                blogTableVM.DateName = "Yan";
            }
            if (blogTableVM.Date.Month == 2)
            {
                blogTableVM.DateName = "Fev";
            }
            if (blogTableVM.Date.Month == 3)
            {
                blogTableVM.DateName = "Mart";
            }
            if (blogTableVM.Date.Month == 4)
            {
                blogTableVM.DateName = "Apr";
            }
            if (blogTableVM.Date.Month ==5)
            {
                blogTableVM.DateName = "May";
            }
            if (blogTableVM.Date.Month == 6)
            {
                blogTableVM.DateName = "Iyun";
            }
            if (blogTableVM.Date.Month == 7)
            {
                blogTableVM.DateName = "Iyul";
            }
            if (blogTableVM.Date.Month == 8)
            {
                blogTableVM.DateName = "Avq";
            }
            if (blogTableVM.Date.Month == 9)
            {
                blogTableVM.DateName = "Sen";
            }
            if (blogTableVM.Date.Month == 10)
            {
                blogTableVM.DateName = "Okt";
            }
            if (blogTableVM.Date.Month == 11)
            {
                blogTableVM.DateName = "Noy";
            }
            if (blogTableVM.Date.Month == 12)
            {
                blogTableVM.DateName = "Dec";
            }
            BlogTable blogTable = new BlogTable { Image = ImageFile, Date = DateTime.Now, PrimaryTitle = blogTableVM.PrimaryTitle , SecondaryTitle=blogTableVM.SecondaryTitle,Name=blogTableVM.DateName };
            _context.BlogTables.Add(blogTable);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0) return BadRequest();
            Blog blog = _context.Blogs.Find(id);
            if (blog is null) return NotFound();
            return View(blog);
        }
        [HttpPost]
        public IActionResult Update(int? id, BlogTable blogTable)
        {
            if (id == null || id == 0 || id != blogTable.Id || blogTable is null) return BadRequest();
            if (!ModelState.IsValid) return View();
            BlogTable exist = _context.BlogTables.Find(blogTable.Id);
            exist.Image = blogTable.Image;
            exist.PrimaryTitle = blogTable.PrimaryTitle;
            exist.SecondaryTitle = blogTable.SecondaryTitle;
            exist.Date = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            BlogTable blogTable = _context.BlogTables.Find(id);
            if (blogTable is null) return NotFound();
            _context.BlogTables.Remove(blogTable);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

