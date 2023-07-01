using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.ViewModels.Product;

namespace SchuseOnlineShop.ViewModels.Shop
{
    public class ShopVM
    {
        public IEnumerable<Models.Product> Products { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Color> Colors { get; set; }
        public IEnumerable<Size> Sizes { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public Paginate<ProductVM> PaginateDatas { get; set; }
    }
}
