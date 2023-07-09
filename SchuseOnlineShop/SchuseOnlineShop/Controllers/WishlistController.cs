using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Data;
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
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public WishlistController(IProductService productService, IWishlistService wishlistService, AppDbContext context, UserManager<AppUser> userManager)
        {
            _productService = productService;
            _wishlistService = wishlistService;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            AppUser user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var wishlist = await _context.Wishlists
               .Include(m => m.WishlistProducts)
               .ThenInclude(m => m.Product)
               .ThenInclude(m => m.Brand)
               .Include(m => m.WishlistProducts)
               .ThenInclude(m => m.Product)
               .ThenInclude(m => m.ProductImages)
               .FirstOrDefaultAsync(m => m.AppUserId == user.Id);


             WishlistIndexVM model = new WishlistIndexVM();

            if (wishlist == null) return View(model);

            foreach (var dbWishlistProducts in wishlist.WishlistProducts)
            {
                WishlistDetailVM wishlistProduct = new WishlistDetailVM
                {
                    Id = dbWishlistProducts.Id,
                    Name = dbWishlistProducts.Product.Name,
                    Image = dbWishlistProducts.Product.ProductImages.FirstOrDefault(m => m.IsMain)?.ImgName,
                    Brand = dbWishlistProducts.Product.Brand,
                    Count = dbWishlistProducts.Count,
                    Price = dbWishlistProducts.Product.DiscountPrice,
                };
                model.WishlistDetails.Add(wishlistProduct);

            }

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> AddWishlist(WishlistAdd wishlistAddVM)
        {
            if (!ModelState.IsValid) return BadRequest(wishlistAddVM);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var product = await _context.Products.FindAsync(wishlistAddVM.Id);
            if (product == null) return NotFound();

            var wishlist = await _context.Wishlists.FirstOrDefaultAsync(m => m.AppUserId == user.Id);

            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    AppUserId = user.Id
                };

                await _context.Wishlists.AddAsync(wishlist);
                await _context.SaveChangesAsync();
            }

            var wishlistProduct = await _context.WishlistProducts
                .FirstOrDefaultAsync(bp => bp.ProductId == product.Id && bp.WishlistId == wishlist.Id);

            if (wishlistProduct != null)
            {
                wishlistProduct.Count++;
            }

            else
            {
                wishlistProduct = new WishlistProduct
                {
                    WishlistId = wishlist.Id,
                    ProductId = product.Id,
                    Count = 1
                };

                await _context.WishlistProducts.AddAsync(wishlistProduct);
            }

            await _context.SaveChangesAsync();
            return Ok(await _context.WishlistProducts.Where(bp => bp.Wishlist.AppUserId == user.Id).SumAsync(bp => bp.Count));
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var wishlistProducts = await _context.WishlistProducts
                .FirstOrDefaultAsync(bp => bp.Id == id
                && bp.Wishlist.AppUserId == user.Id);

            if (wishlistProducts == null) return NotFound();

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == wishlistProducts.ProductId);
            if (product == null) return NotFound();




            _context.WishlistProducts.Remove(wishlistProducts);
            await _context.SaveChangesAsync();
            return Ok(await _context.WishlistProducts.Where(bp => bp.Wishlist.AppUserId == user.Id).SumAsync(bp => bp.Count));
        }


    }


}
