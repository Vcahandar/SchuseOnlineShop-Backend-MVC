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

        public void DeleteData(int? id)
        {
            var baskets = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            var deletedProduct = baskets.FirstOrDefault(b => b.ProductId == id);
            baskets.Remove(deletedProduct);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets));
        }

        public async Task<List<CartProduct>> GetAllByCartIdAsync(int? cartId)
        {
            return await _context.CartProducts.Where(m => m.CartId == cartId).ToListAsync();
        }

        public async Task<Cart> GetByUserIdAsync(string userId)
        {
            return await _context.Carts.Include(m => m.CartProducts).FirstOrDefaultAsync(m => m.AppUserId == userId);
        }

        public List<CartVM> GetDatasFromCookie()
        {
            List<CartVM> carts;

            if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                carts = JsonConvert.DeserializeObject<List<CartVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                carts = new List<CartVM>();
            }
            return carts;
        }

        public void SetDatasToCookie(List<CartVM> carts, Product dbProduct, CartVM existProduct)
        {
            if (existProduct == null)
            {
                carts.Add(new CartVM
                {
                    ProductId = dbProduct.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }
            _httpContextAccessor.HttpContext.Response.Cookies
                .Append("basket", JsonConvert.SerializeObject(carts));
        }
    }
}
