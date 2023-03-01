using Autoparts.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Autoparts.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DashboardController: Controller
    {        
        public IActionResult Index()
        {
            return View();
        }
    }
}
