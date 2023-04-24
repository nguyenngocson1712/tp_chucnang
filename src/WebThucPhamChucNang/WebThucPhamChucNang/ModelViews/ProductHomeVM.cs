using WebThucPhamChucNang.Models;

namespace WebThucPhamChucNang.ModelViews
{
    public class ProductHomeVM
    {
        public Category category { get; set; }
        public List<Product> lsProducts { get; set; }
    }
}
