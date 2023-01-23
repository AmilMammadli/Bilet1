using Microsoft.AspNetCore.Mvc;

namespace Indigo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
