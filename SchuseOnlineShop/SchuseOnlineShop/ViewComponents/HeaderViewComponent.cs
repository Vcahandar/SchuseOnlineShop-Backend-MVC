using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Services.Interfaces;
using SchuseOnlineShop.ViewModels.Layout;

namespace SchuseOnlineShop.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;
        private readonly ICartService _cartService;
        private readonly IWishlistService _wishlistService;

        public HeaderViewComponent(ILayoutService layoutService, 
                                    ICartService cartService, 
                                    IWishlistService wishlistService)
        {
            _layoutService = layoutService;
            _cartService = cartService;
            _wishlistService = wishlistService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            LayoutVM model = new()
            {
                GetSettingDatas = _layoutService.GetSettings(),
                BasketCount = _cartService.GetDatasFromCookie().Count,
                WishlistCount = _wishlistService.GetDatasFromCookie().Count

            };
            return await Task.FromResult(View(model));
        }
    }
}
