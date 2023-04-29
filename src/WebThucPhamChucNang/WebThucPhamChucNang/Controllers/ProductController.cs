using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using WebThucPhamChucNang.Models;

namespace WebThucPhamChucNang.Controllers
{
    public class ProductController : Controller
    {
        private readonly DbMarketsContext _context;
        public ProductController(DbMarketsContext context)
        {
            _context = context;
        }
        [Route("shop.html", Name = ("ShopProduct"))]
        public async Task<IActionResult>  Index(int? page, int catid=0, string keyword="")
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 10;
                ViewBag.CurrentPage = pageNumber;
                List<Product> products = new List<Product>();
                if (catid != 0)
                {
                    products = await _context.Products
                        .Include(x => x.Cat)
                        .Where(x => x.CatId == catid)
                        .AsNoTracking()
                        .OrderByDescending(x => x.ProductId)
                        .ToListAsync();
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        products = products.Where(x => x.ProductName == keyword).ToList();
                    }
                }
                else
                {
                    products = await _context.Products
                         .Include(x => x.Cat)

                        .AsNoTracking()
                        .OrderByDescending(x => x.ProductId)
                        .ToListAsync();
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        products = products.Where(x => x.ProductName == keyword).ToList();
                    }
                }
               
                PagedList<Product> models = new PagedList<Product>(products.AsQueryable(), pageNumber, pageSize);
                ViewBag.CurrentCatid = catid;
                ViewBag.Currentkeyword = keyword;
                ViewData["DanhMuc"] = new SelectList(_context.Categories, "CatId", "CatName", catid);
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

            
        }
        //public IActionResult Filtter(int CatID = 0)
        //{
        //    var url = $"/Product?CatID={CatID}";
        //    if (CatID == 0)
        //    {
        //        url = $"/Product";
        //    }
        //    return Json(new { status = "success", redirectUrl = url });
        //}
        [Route("danhmuc/{Alias}", Name = ("ListProduct"))]
        public IActionResult List(string Alias, int page = 1)
        {
            try
            {
                var pageSize = 10;
                var danhmuc = _context.Categories.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);

                var lsTinDangs = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CatId == danhmuc.CatId)
                    .OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(lsTinDangs, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCat = danhmuc;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }


        }
        [Route("/{Alias}-{id}.html", Name = ("ProductDetails"))]
        public IActionResult Details(int id)
        {
            try
            {
                var product = _context.Products.Include(x => x.Cat).FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                var lsProduct = _context.Products
                        .AsNoTracking()
                        .Where(x => x.CatId == product.CatId && x.ProductId != id && x.Active == true)
                        .Take(4)
                        .OrderByDescending(x => x.DateCreated)
                        .ToList();
                ViewBag.SanPham = lsProduct;
                return View(product);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }



        }
    }
}

