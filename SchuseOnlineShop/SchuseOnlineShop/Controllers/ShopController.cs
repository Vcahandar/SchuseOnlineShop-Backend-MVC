using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;
using SchuseOnlineShop.ViewModels.Cart;
using SchuseOnlineShop.ViewModels.Product;
using SchuseOnlineShop.ViewModels.Shop;

namespace SchuseOnlineShop.Controllers
{
    public class ShopController : Controller
    {
       
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICategoryService _categoryService;
        private readonly IColorService _colorService;
        private readonly IBrandService _branService;
        private readonly ICrudService<ProductComment> _crudService;
        private readonly ILayoutService _layoutService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly ISizeService _sizeService;

        public ShopController(IProductService productService,
            ICartService cartService, 
            ICategoryService categoryService, 
            ISubCategoryService subCategoryService,
            IColorService colorService, 
            IBrandService branService, 
            ICrudService<ProductComment> crudService,
            ILayoutService layoutService,
            ISizeService sizeService)
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
        }

        public async Task<IActionResult> Index(int page = 1, int take = 6, int? subcategoryId = null, int? colorId = null, int? brandId = null,int? sizeId = null)
        {
            List<Product> datas = await _productService.GetPaginatedDatasAsync(page, take, subcategoryId, colorId, brandId,sizeId);
            List<ProductVM> mappedDatas = GetDatas(datas);
            int pageCount = 0;
            ViewBag.subCatId = subcategoryId;
            ViewBag.branId = brandId;
            ViewBag.sizeId = sizeId;

            if (subcategoryId != null)
            {
                pageCount = await GetPageCountAsync(take, subcategoryId, null, null,null);
            }
            if (colorId != null)
            {
                pageCount = await GetPageCountAsync(take, null, colorId, null,null);
            }

            if (brandId != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, brandId,null);
            }

            if (sizeId != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, sizeId);
            }

            if (subcategoryId == null && brandId == null && colorId == null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, null);
            }

            Paginate<ProductVM> paginatedDatas = new(mappedDatas, page, pageCount);

            ShopVM model = new()
            {
                Products = await _productService.GetFullDataAsync(),
                SubCategories = await _subCategoryService.GetAllAsync(),
                Brands = await _branService.GetAllAsync(),
                Colors = await _colorService.GetAllAsync(),
                Sizes = await _sizeService.GetAllAsync(),
                PaginateDatas = paginatedDatas
            };
            return View(model);
        }

        private async Task<int> GetPageCountAsync(int take, int? subCatId, int? colorId, int? branId,int? sizeId)
        {
            int prodCount = 0;
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
            if (subCatId == null && branId == null && colorId == null && sizeId == null)
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

            int pageCount = await GetPageCountAsync(take, (int)id, null, null,null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }


        [HttpGet]
        public async Task<IActionResult> GetProductsByBrand(int? id, int page = 1, int take = 6)
        {
            if (id is null) return BadRequest();
            ViewBag.brandId = id;
            var products = await _productService.GetProductsByColorIdAsync(id);
            int pageCount = await GetPageCountAsync(take, null, null, (int)id, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByColor(int? id, int page = 1, int take = 6)
        {
            if (id is null) return BadRequest();
            ViewBag.colorId = id;

            var products = await _productService.GetProductsByColorIdAsync(id);

            int pageCount = await GetPageCountAsync(take, null, (int)id, null, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsBySize(int? id, int page = 1, int take = 6)
        {
            if (id is null) return BadRequest();
            ViewBag.sizeId = id;

            var products = await _productService.GetProductsBySizeIdAsync(id);

            int pageCount = await GetPageCountAsync(take, null, null, null, (int)id);

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
                    DiscountPrice = dbProduct.DiscountPrice,
                    SubCategories = dbProduct.SubCategory,
                    Categories = dbProduct.Category,
                    ProductColors = dbProduct.ProductColors,
                    ProductImages = dbProduct.ProductImages,
                    ProductVideos = dbProduct.ProductVideos,
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

        [HttpPost]
        public async Task<IActionResult> AddToCart(int? id)
        {
            if (id is null) return BadRequest();

            Product dbProduct = await _productService.GetByIdAsync((int)id);

            if (dbProduct == null) return NotFound();

            List<CartVM> carts = _cartService.GetDatasFromCookie();

            CartVM existProduct = carts.FirstOrDefault(p => p.ProductId == id);

            _cartService.SetDatasToCookie(carts, dbProduct, existProduct);

            int cartCount = carts.Count;

            return Ok(cartCount);
        }

        public async Task<IActionResult> Search(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return Ok();
            }
            var products = await _productService.GetAllBySearchText(searchText);

            return View(products);
        }





        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(int? id, int page = 1, int take = 5)
        {
            if (id is null) return BadRequest();
            ViewBag.catId = id;

            var products = await _productService.GetProductsByCategoryIdAsync(id, page, take);

            int pageCount = await GetPageCountAsync(take, (int)id, null, null, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

    }
}
