using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;
using SchuseOnlineShop.ViewModels.Cart;
using SchuseOnlineShop.ViewModels.Product;
using SchuseOnlineShop.ViewModels.Shop;
using SchuseOnlineShop.ViewModels.Wishlist;

namespace SchuseOnlineShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICategoryService _categoryService;
        private readonly IColorService _colorService;
        private readonly IBrandService _branService;
        private readonly ICrudService<ProductComment> _crudService;
        private readonly ILayoutService _layoutService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly ISizeService _sizeService;
        private readonly IWishlistService _wishlistService;

        public ShopController(IProductService productService,
            ICartService cartService, 
            ICategoryService categoryService, 
            ISubCategoryService subCategoryService,
            IColorService colorService, 
            IBrandService branService, 
            ICrudService<ProductComment> crudService,
            ILayoutService layoutService,
            ISizeService sizeService,
            IWishlistService wishlistService,
            AppDbContext context)
        {
            _productService = productService;
            _cartService = cartService;
            _categoryService = categoryService;
            _colorService = colorService;
            _branService = branService;
            _crudService = crudService;
            _layoutService = layoutService;
            _subCategoryService = subCategoryService;
            _sizeService = sizeService;
            _wishlistService = wishlistService;
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 6, int? categoryId = null, int? subcategoryId = null, int? colorId = null, int? brandId = null,int? sizeId = null)
        {
            List<Product> datas = await _productService.GetPaginatedDatasAsync(page, take, categoryId, subcategoryId, colorId, brandId,sizeId);
            List<ProductVM> mappedDatas = GetDatas(datas);
            int pageCount = 0;
            ViewBag.catId = categoryId;
            ViewBag.subCatId = subcategoryId;
            ViewBag.branId = brandId;
            ViewBag.sizeId = sizeId;



            if (categoryId != null)
            {
                pageCount = await GetPageCountAsync(take, categoryId, null, null, null, null);
            }
            if (subcategoryId != null)
            {
                pageCount = await GetPageCountAsync(take, null, subcategoryId, null, null,null);
            }
            if (colorId != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, colorId, null,null);
            }

            if (brandId != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, brandId,null);
            }

            if (sizeId != null)
            {
                pageCount = await GetPageCountAsync(take, null,null, null, null, sizeId);
            }

            if (categoryId == null && subcategoryId == null && brandId == null && colorId == null)
            {
                pageCount = await GetPageCountAsync(take, null,null, null, null, null);
            }

            Paginate<ProductVM> paginatedDatas = new(mappedDatas, page, pageCount);

            ShopVM model = new()
            {
                Products = await _productService.GetFullDataAsync(),
                SubCategories = await _subCategoryService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync(),
                Brands = await _branService.GetAllAsync(),
                Colors = await _colorService.GetAllAsync(),
                Sizes = await _sizeService.GetAllAsync(),
                PaginateDatas = paginatedDatas
            };
            return View(model);
        }

        private async Task<int> GetPageCountAsync(int take, int? catId, int? subCatId, int? colorId, int? branId, int? sizeId)
        {
            int prodCount = 0;
            if (catId is not null)
            {
                prodCount = await _productService.GetProductsCountByCategoryAsync(catId);
            }
            if (subCatId is not null)
            {
                prodCount = await _productService.GetProductsCountBySubCategoryAsync(subCatId);
            }
            if (colorId is not null)
            {
                prodCount = await _productService.GetProductsCountByColorAsync(colorId);

            }
            if (branId is not null)
            {
                prodCount = await _productService.GetProductsCountByBrandAsync(branId);

            }
            if (sizeId is not null)
            {
                prodCount = await _productService.GetProductsCountBySizeAsync(sizeId);

            }
            if (catId == null && subCatId == null && branId == null && colorId == null && sizeId == null)
            {
                prodCount = await _productService.GetCountAsync();
            }

            return (int)Math.Ceiling((decimal)prodCount / take);
        }

        private List<ProductVM> GetDatas(List<Product> products)
        {
            List<ProductVM> mappedDatas = new();
            foreach (var product in products)
            {
                ProductVM productList = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    DiscountPrice = product.DiscountPrice,
                    ProductImages = product.ProductImages,
                    Rating = product.Rating
                };
                mappedDatas.Add(productList);
            }
            return mappedDatas;
        }


        [HttpGet]
        public async Task<IActionResult> GetProductsBySubCategory(int? id, int page = 1, int take = 6)
        {
            if (id is null) return BadRequest();
            ViewBag.subCatId = id;

            var products = await _productService.GetProductsBySubCategoryIdAsync(id, page, take);

            int pageCount = await GetPageCountAsync(take, null, (int)id, null, null, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }


        [HttpGet]
        public async Task<IActionResult> GetProductsByBrand(int? id, int page = 1, int take = 6)
        {
            if (id is null) return BadRequest();
            ViewBag.brandId = id;
            var products = await _productService.GetProductsByBrandIdAsync(id);
            int pageCount = await GetPageCountAsync(take, null, null, null, (int)id, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByColor(int? id, int page = 1, int take = 6)
        {
            if (id is null) return BadRequest();
            ViewBag.colorId = id;

            var products = await _productService.GetProductsByColorIdAsync(id);

            int pageCount = await GetPageCountAsync(take, null, null, (int)id, null, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsBySize(int? id, int page = 1, int take = 6)
        {
            if (id is null) return BadRequest();
            ViewBag.sizeId = id;

            var products = await _productService.GetProductsBySizeIdAsync(id);

            int pageCount = await GetPageCountAsync(take, null,null, null, null, (int)id);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }



        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetDatasAsync();
            return PartialView("_ProductListPartial", products);
        }


        [HttpGet]
        public async Task<IActionResult> ProductDetail(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                var dbProduct = await _productService.GetFullDataByIdAsync((int)id);
                if (dbProduct is null) return NotFound();

                ProductDetailVM model = new()
                {
                    Id = dbProduct.Id,
                    ProductName = dbProduct.Name,
                    Price = dbProduct.Price,
                    Video =dbProduct.Video,
                    DiscountPrice = dbProduct.DiscountPrice,
                    SubCategories = dbProduct.SubCategory,
                    Categories = dbProduct.Category,
                    ProductColors = dbProduct.ProductColors,
                    ProductImages = dbProduct.ProductImages,
                    ProductSizes = dbProduct.ProductSizes,
                    SKU = dbProduct.SKU,
                    Rating = dbProduct.Rating,
                    Description = dbProduct.Description,
                    BrandName = dbProduct.Brand.Name,
                    ProductCommentVM = new(),
                    ProductComments = dbProduct.ProductComments
                };

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PostComment(ProductDetailVM model, int? id, string userId)
        {
            if (id is null || userId == null) return BadRequest();
            if (!ModelState.IsValid) return RedirectToAction(nameof(ProductDetail), new { id });

            ProductComment productComment = new()
            {
                Name = model.ProductCommentVM.Name,
                Email = model.ProductCommentVM.Email,
                Subject = model.ProductCommentVM.Subject,
                Message = model.ProductCommentVM.Message,
                AppUserId = userId,
                ProductId = (int)id
            };
            await _crudService.CreateAsync(productComment);
            return RedirectToAction(nameof(ProductDetail), new { id });
        }


        [HttpPost]
        public async Task<IActionResult> Filter(string value)
        {
            if (value is null) return BadRequest();
            var products = await _productService.GetAllAsync();
            switch (value)
            {
                case "Sort by Default":
                    products = products;
                    break;
                case "Sort by Popularity":
                    products = products.OrderByDescending(p => p.SaleCount);
                    break;
                case "Sort by Rated":
                    products = products.OrderByDescending(p => p.Rating);
                    break;
                case "Sort by Latest":
                    products = products.OrderByDescending(p => p.CreatedDate);
                    break;
                case "Sort by High Price":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "Sort by Low Price":
                    products = products.OrderBy(p => p.Price);
                    break;
                default:
                    break;
            }
            return PartialView("_ProductListPartial", products);

        }

        //[HttpPost]
        //public async Task<IActionResult> AddToCart(int? id)
        //{
        //    if (id is null) return BadRequest();

        //    Product dbProduct = await _productService.GetByIdAsync((int)id);

        //    if (dbProduct == null) return NotFound();

        //    List<CartVM> carts = _cartService.GetDatasFromCookie();

        //    CartVM existProduct = carts.FirstOrDefault(p => p.ProductId == id);

        //    _cartService.SetDatasToCookie(carts, dbProduct, existProduct);

        //    int cartCount = carts.Count;

        //    return Ok(cartCount);
        ////}

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int? id)
        {
            if (id is null) return BadRequest();

            Product dbProduct = await _productService.GetByIdAsync((int)id);

            if (dbProduct == null) return NotFound();

            List<WishlistVM> wishlists = _wishlistService.GetDatasFromCookie();

            WishlistVM existProduct = wishlists.FirstOrDefault(p => p.ProductId == id);

            _wishlistService.SetDatasToCookie(wishlists, dbProduct, existProduct);

            int cartCount = wishlists.Count;

            return Ok(cartCount);
        }

        public async Task<IActionResult> Search(string searchText)
        {

            var productsAll = await _context.Products
                             .Include(m => m.ProductImages)
                             .Include(m => m.Category)?
                             .OrderByDescending(m => m.Id)
                             .Where(m => !m.SoftDelete && m.Name.ToLower().Trim().Contains(searchText.ToLower().Trim()))
                             .Take(6)
                             .ToListAsync();

            return View(productsAll);
        }



        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(int? id, int page = 1, int take = 6)
        {
            if (id is null) return BadRequest();
            ViewBag.catId = id;

            var products = await _productService.GetProductsByCategoryIdAsync(id, page, take);

            int pageCount = await GetPageCountAsync(take, (int)id, null, null, null, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }



        [HttpPost]
        public async Task<IActionResult> GetDataProductModal(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                var dbProduct = await _productService.GetDatasModalProductByIdAsyc((int)id);
                if (dbProduct is null) return NotFound();

                ModalVM model = new()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Price = dbProduct.Price,
                    Description = dbProduct.Description,
                    Brand = dbProduct.Brand.Name,
                    Sku = dbProduct.SKU,
                    Category = dbProduct.Category.Name,
                    ProductImages = dbProduct.ProductImages,
                };

                return Ok(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



    }
}
