using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.ViewModels.Home
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<BrandLogo> BrandLogos { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<HomeCategory> HomeCategories { get; set; }


    }
}
