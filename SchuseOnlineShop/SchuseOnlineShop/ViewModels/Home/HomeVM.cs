using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services;

namespace SchuseOnlineShop.ViewModels.Home
{
    public class HomeVM
    {
        public int Id { get; set; }
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<BrandLogo> BrandLogos { get; set; }
        public IEnumerable<Models.Blog> Blogs { get; set; }
        public IEnumerable<HomeCategory> HomeCategories { get; set; }
        public IEnumerable<Models.Product> Products { get; set; }
        public List<SubCategoryService> SubCategories { get; set; }


    }
}
