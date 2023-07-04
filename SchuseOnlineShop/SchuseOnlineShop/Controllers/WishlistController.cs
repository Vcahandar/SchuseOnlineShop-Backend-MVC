using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services;
using SchuseOnlineShop.Services.Interfaces;
using SchuseOnlineShop.ViewModels.Cart;
using SchuseOnlineShop.ViewModels.Wishlist;

namespace SchuseOnlineShop.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWishlistService _wishlistService;
        public WishlistController(IProductService productService, IWishlistService wishlistService)
        {
            _productService = productService;
            _wishlistService = wishlistService;
        }

        public async Task<IActionResult> Index()
        {
            List<WishlistVM> wishlists = _wishlistService.GetDatasFromCookie();
            List<WishlistDetailVM> wishlistDetailVMs = new();
            foreach (var item in wishlists)
            {
                Product dbProduct = await _productService.GetByIdAsync((int)item.ProductId);

                wishlistDetailVMs.Add(new WishlistDetailVM()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Price = dbProduct.DiscountPrice,
                    Image = dbProduct.ProductImages.FirstOrDefault(i => i.IsMain).ImgName,
                    Count = item.Count,
                });
            }
            return View(wishlistDetailVMs);
        }

        [HttpPost]
        public IActionResult DeleteDataFromWishlist(int? id)
        {
            if (id is null) return BadRequest();

            _wishlistService.DeleteData((int)id);
            List<WishlistVM> wishlists = _wishlistService.GetDatasFromCookie();


            return Ok(wishlists.Count);

        }
    }
}
