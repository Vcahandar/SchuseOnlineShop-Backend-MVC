using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;
using SchuseOnlineShop.ViewModels.Layout;
using System.Security.Claims;

namespace SchuseOnlineShop.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWishlistService _wishlistService;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public HeaderViewComponent(ILayoutService layoutService, 
                                    IWishlistService wishlistService, 
                                    UserManager<AppUser> userManager,
                                    AppDbContext context,
                                    IHttpContextAccessor httpContextAccessor)
        {
            _layoutService = layoutService;
            _wishlistService = wishlistService;
            _userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<int> GetUserBasketProductsCount(ClaimsPrincipal userClaims)
        {
            var user = await _userManager.GetUserAsync(userClaims);
            if (user == null) return 0;
            var basketProductCount = await _context.CartProducts.Where(bp => bp.Cart.AppUserId == user.Id).SumAsync(bp => bp.Count);
            return basketProductCount;
        }

        public async Task<int> GetUserWihslistProductsCount(ClaimsPrincipal userClaims)
        {
            var user = await _userManager.GetUserAsync(userClaims);
            if (user == null) return 0;
            var wishlistProductCount = await _context.WishlistProducts.Where(bp => bp.Wishlist.AppUserId == user.Id).SumAsync(bp => bp.Count);
            return wishlistProductCount;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            LayoutVM model = new()
            {
                GetSettingDatas = _layoutService.GetSettings(),
                BasketCount = await GetUserBasketProductsCount(_httpContextAccessor.HttpContext.User),
                WishlistCount = await GetUserWihslistProductsCount(_httpContextAccessor.HttpContext.User),
                Categories = await _layoutService.GetCategorysByName()
            };
            return await Task.FromResult(View(model));
        }
    }
}
