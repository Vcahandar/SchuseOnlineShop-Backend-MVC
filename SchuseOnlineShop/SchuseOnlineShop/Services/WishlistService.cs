using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;
using SchuseOnlineShop.ViewModels.Cart;
using SchuseOnlineShop.ViewModels.Wishlist;

namespace SchuseOnlineShop.Services
{
    public class WishlistService : IWishlistService
    {
        private AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WishlistService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public void DeleteData(int? id)
        {
            var wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(_httpContextAccessor.HttpContext.Request.Cookies["wishlist"]);
            var deletedProduct = wishlist.FirstOrDefault(m => m.ProductId == id);
            wishlist.Remove(deletedProduct);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlist));
        }

        public async Task<List<WishlistProduct>> GetAllByWishlistIdAsync(int? wishlistId)
        {
            return await _context.WishlistProducts.Where(m => m.WishlistId == wishlistId).ToListAsync();
        }

        public async Task<Wishlist> GetByUserIdAsync(string userId)
        {
            return await _context.Wishlists.Include(m => m.WishlistProducts).FirstOrDefaultAsync(m => m.AppUserId == userId);
        }

        public List<WishlistVM> GetDatasFromCookie()
        {
            List<WishlistVM> carts;

            if (_httpContextAccessor.HttpContext.Request.Cookies["wishlist"] != null)
            {
                carts = JsonConvert.DeserializeObject<List<WishlistVM>>(_httpContextAccessor.HttpContext.Request.Cookies["wishlist"]);
            }
            else
            {
                carts = new List<WishlistVM>();
            }
            return carts;
        }

        public void SetDatasToCookie(List<WishlistVM> wishlists, Product dbProduct, WishlistVM existProduct)
        {
            if (existProduct == null)
            {
                wishlists.Add(new WishlistVM
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
                .Append("wishlist", JsonConvert.SerializeObject(wishlists));
        }
    }
}
