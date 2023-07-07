using SchuseOnlineShop.Models;
using SchuseOnlineShop.ViewModels.Cart;

namespace SchuseOnlineShop.Services.Interfaces
{
    public interface ICartService
    {
        Task<Cart> GetByUserIdAsync(string userId);
        Task<List<CartProduct>> GetAllByCartIdAsync(int? cartId);
    }
}
