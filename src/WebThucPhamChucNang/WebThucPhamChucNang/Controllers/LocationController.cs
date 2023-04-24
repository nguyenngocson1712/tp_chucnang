using Microsoft.AspNetCore.Mvc;
using WebThucPhamChucNang.Models;

namespace WebThucPhamChucNang.Controllers
{
    public class LocationController : Controller
    {
        private readonly DbMarketsContext _context;
        public LocationController(DbMarketsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult QuanHuyenList(int LocationId)
        {
            var QuanHuyens = _context.Locations.OrderBy(x => x.LocationId)
                .Where(x => x.Parent == LocationId && x.Levels==2)

            .OrderBy(x => x.Name)

            .ToList();

            return Json(QuanHuyens);

        }
        public ActionResult PhuongXaList(int LocationId)
        {
            var PhuongXas = _context.Locations.OrderBy(x => x.LocationId)
                .Where(x => x.Parent == LocationId && x.Levels==3)

            .OrderBy(x => x.Name)

            .ToList();

            return Json(PhuongXas);

        }
    }
}
