using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.ViewModels.Shop
{
    public class FilterViewVM
    {
        public IEnumerable<Models.Product> Products { get; set; }
        public IEnumerable<Size> Sizes { get; set; }
        public IEnumerable<Brand> Brands { get; set; } 
    }
}
