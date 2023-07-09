using SchuseOnlineShop.ViewModels.Cart;

namespace SchuseOnlineShop.ViewModels.Wishlist
{
    public class WishlistIndexVM
    {
        public WishlistIndexVM()
        {
            WishlistDetails = new List<WishlistDetailVM>();
        }
        public List<WishlistDetailVM> WishlistDetails { get; set; }
    }
}
