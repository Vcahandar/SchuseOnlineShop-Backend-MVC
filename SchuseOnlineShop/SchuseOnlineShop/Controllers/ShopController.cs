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

        public async Task<IActionResult> Index(int page = 1, int take = 6, int? categoryId = null, int? subcategoryId = null, int? colorId = null, int? brandId = null, int? minValue = null, int? maxValue = null)
        {
            List<Product> datas = await _productService.GetPaginatedDatasAsync(page, take, categoryId, subcategoryId, colorId, brandId, minValue, maxValue);
            List<ProductVM> mappedDatas = GetDatas(datas);
            int pageCount = 0;
            ViewBag.catId = categoryId;
            ViewBag.subCatId = subcategoryId;
            ViewBag.colorId = colorId;
            ViewBag.branId = brandId;
            ViewBag.minValue = minValue;
            ViewBag.maxValue = maxValue;



            if (categoryId != null)
            {
                pageCount = await GetPageCountAsync(take, categoryId, null, null, null, null, null);
            }
            if (subcategoryId != null)
            {
                pageCount = await GetPageCountAsync(take, null, subcategoryId, null, null , null, null);
            }
            if (colorId != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, colorId, null, null, null);
            }

            if (brandId != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, brandId, null, null);
            }

            if (minValue != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, null, minValue, null);
            }


            if (maxValue != null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, null, null, maxValue);
            }



            if (categoryId == null && subcategoryId == null && brandId == null && colorId == null && minValue == null && maxValue == null)
            {
                pageCount = await GetPageCountAsync(take, null, null, null, null, null, null);
            }

            Paginate<ProductVM> paginatedDatas = new(mappedDatas, page, pageCount);

            ShopVM model = new()
            {
                Products = await _productService.GetFullDataAsync(),
                SubCategories = await _subCategoryService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync(),
                Brands = await _branService.GetAllAsync(),
                Colors = await _colorService.GetAllAsync(),
                PaginateDatas = paginatedDatas
            };
            return View(model);
        }




        private async Task<int> GetPageCountAsync(int take, int? catId, int? subCatId, int? colorId, int? branId, int? minValue, int? maxValue)
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
            if (minValue != null && maxValue != null)
            {
                prodCount = await _productService.GetProductsCountByRangeAsync(minValue,maxValue);

            }

            if (catId == null && subCatId == null && branId == null && colorId == null && minValue == null && maxValue == null)
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

            int pageCount = await GetPageCountAsync(take, null, (int)id, null, null, null ,null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }


        [HttpGet]
        public async Task<IActionResult> GetProductsByBrand(int? id, int page = 1, int take = 6)
        {
            if (id is null) return BadRequest();
            ViewBag.brandId = id;
            var products = await _productService.GetProductsByBrandIdAsync(id, page, take);
            int pageCount = await GetPageCountAsync(take, null, null, null, (int)id , null ,null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByColor(int? id, int page = 1, int take = 6)
        {
            if (id is null) return BadRequest();
            ViewBag.colorId = id;

            var products = await _productService.GetProductsByColorIdAsync(id ,page, take);

            int pageCount = await GetPageCountAsync(take, null, null, (int)id, null ,null ,null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }

        [HttpGet]



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

            int pageCount = await GetPageCountAsync(take, (int)id, null, null, null, null, null);

            Paginate<ProductVM> model = new(products, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }


        public async Task<IActionResult> ProductSort(string? sortValue, int page = 1, int take = 6)
        {
            ViewBag.sortValue = sortValue;

            List<Product> products = new();

            if (sortValue == "1")
            {
                products = await _context.Products.Include(m => m.ProductImages).ToListAsync();
            };
            if (sortValue == "2")
            {
                products = await _context.Products.Include(m => m.ProductImages).OrderByDescending(p => p.Rating).ToListAsync();

            };
            if (sortValue == "3")
            {
                products = await _context.Products.Include(m => m.ProductImages).OrderByDescending(p => p.CreatedDate).ToListAsync();

            };
            if (sortValue == "4")
            {
                products = await _context.Products.Include(m => m.ProductImages).OrderByDescending(p => p.Price).ToListAsync();

            };
            if (sortValue == "5")
            {
                products = await _context.Products.Include(m => m.ProductImages).OrderBy(p => p.Price).ToListAsync();

            };
            int productCount = products.Count();
            var pageCount = (int)Math.Ceiling((decimal)productCount / take);
            List<ProductVM> mappedDatas = GetDatas(products);
            Paginate<ProductVM> model = new(mappedDatas, page, pageCount);

            return PartialView("_ProductListPartial", model);
        }





        //[HttpPost]
        //public async Task<IActionResult> GetDataProductModal(int? id)
        //{
        //    try
        //    {
        //        if (id is null) return BadRequest();
        //        var dbProduct = await _productService.GetDatasModalProductByIdAsyc((int)id);
        //        if (dbProduct is null) return NotFound();

        //        ModalVM model = new()
        //        {
        //            Id = dbProduct.Id,
        //            Name = dbProduct.Name,
        //            Price = dbProduct.Price,
        //            Description = dbProduct.Description,
        //            Brand = dbProduct.Brand.Name,
        //            Sku = dbProduct.SKU,
        //            Category = dbProduct.Category.Name,
        //            ProductImages = dbProduct.ProductImages,
        //        };

        //        return PartialView("_ModalListPartial",model);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.error = ex.Message;
        //        return View();
        //    }
        //}



        [HttpGet]
        public async Task<IActionResult> GetRangeProducts(int value1, int value2, int page = 1, int take = 6)
        {
            List<Product> products = await _context.Products.Where(m => m.DiscountPrice >= value1 && m.DiscountPrice <= value2).Include(m => m.ProductImages).ToListAsync();
            var productCount = products.Count();
            var pageCount = (int)Math.Ceiling((decimal)productCount / take);
            List<ProductVM> mappedDatas = GetDatas(products);
            Paginate<ProductVM> paginatedDatas = new(mappedDatas, page, pageCount);
            return PartialView("_ProductListPartial", paginatedDatas);
        }

    }
}
