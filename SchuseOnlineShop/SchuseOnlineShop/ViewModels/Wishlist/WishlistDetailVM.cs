using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.ViewModels.Wishlist
{
    public class WishlistDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public Brand Brand { get; set; }

        public int Count { get; set; }
    }
}
