using Microsoft.AspNetCore.Mvc;

namespace WebThucPhamChucNang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
