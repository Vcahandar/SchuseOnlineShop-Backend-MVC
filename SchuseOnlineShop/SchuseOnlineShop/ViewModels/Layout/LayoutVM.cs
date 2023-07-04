using SchuseOnlineShop.Models;
using SchuseOnlineShop.ViewModels.Cart;
using SchuseOnlineShop.ViewModels.Shop;

namespace SchuseOnlineShop.ViewModels.Layout
{
    public class LayoutVM
    {
        public Dictionary<string, string> GetSettingDatas { get; set; }
        public IEnumerable<ShopVM> ShopVMs { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public int BasketCount { get; set; }
        public int WishlistCount { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int ProCount { get; set; }
        public decimal Subtotal { get; set; }
    }
}
