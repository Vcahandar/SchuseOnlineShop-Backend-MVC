using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;
using SchuseOnlineShop.ViewModels.Cart;

namespace SchuseOnlineShop.Services
{
    public class CartService : ICartService
    {
        private AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }



        public async Task<List<CartProduct>> GetAllByCartIdAsync(int? cartId)
        {
            return await _context.CartProducts.Where(m => m.CartId == cartId).ToListAsync();
        }

        public async Task<Cart> GetByUserIdAsync(string userId)
        {
            return await _context.Carts.Include(m => m.CartProducts).FirstOrDefaultAsync(m => m.AppUserId == userId);
        }


    }
}
