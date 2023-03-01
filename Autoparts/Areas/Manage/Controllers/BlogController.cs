using Autoparts.DAL;
using Autoparts.Models;
using Autoparts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;

namespace Autoparts.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BlogController: Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext conntext, IWebHostEnvironment env)
        {
            _context = conntext;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Blogs.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogVM blogVM)
        {
            if (!ModelState.IsValid) return View();
            IFormFile file = blogVM.Image;
            if (!file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekil deyil.");
                return View();
            }
            if (!(blogVM.Image.Length / 1024 / 1024 < 2))
            {
                ModelState.AddModelError("Image", "Sizin gonderdiyiniz sekilin olcusu 2mb-dan coxdur.");
                return View();
            }
            string ImageFile =Guid.NewGuid()+file.FileName;
            using(FileStream stream =new FileStream(Path.Combine(_env.WebRootPath,"image","catalog","blog", "NewBlogImage",ImageFile),FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Blog blog =new Blog { Image=ImageFile , Date = DateTime.Now, Title=blogVM.Title};
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int?id)
        {
            if (id == null || id == 0) return BadRequest();
            Blog blog= _context.Blogs.Find(id);
            if (blog is null) return NotFound();
            return View(blog);
        }
        [HttpPost]
        public IActionResult Update(int? id, Blog blog)
        {
            if (id == null || id == 0 || id != blog.Id || blog is null) return BadRequest();
            if (!ModelState.IsValid) return View();
            Blog exist = _context.Blogs.Find(blog.Id);
            exist.Image = blog.Image;
            exist.Title=blog.Title;
            exist.Date = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            Blog blog = _context.Blogs.Find(id);
            if (blog is null) return NotFound();
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
