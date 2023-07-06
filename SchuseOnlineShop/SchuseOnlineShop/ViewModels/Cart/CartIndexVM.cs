namespace SchuseOnlineShop.ViewModels.Cart
{
    public class CartIndexVM
    {
        public CartIndexVM()
        {
            CartDetails = new List<CartDetailVM>();
        }

        public List<CartDetailVM> CartDetails { get; set; }
    }
}
