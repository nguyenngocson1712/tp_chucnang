using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebThucPhamChucNang.Extension;
using WebThucPhamChucNang.ModelViews;

namespace WebThucPhamChucNang.Controllers.Components
{
    public class NumberCartViewComponent :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            return View(cart);
        }
    }

}
