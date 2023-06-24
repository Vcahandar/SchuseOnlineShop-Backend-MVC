using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;
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
            ViewBag.catId = subcategoryId;
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

        private async Task<int> GetPageCountAsync(int take, int? catId, int? colorId, int? branId,int? sizeId)
        {
            int prodCount = 0;
            if (catId is not null)
            {
                prodCount = await _productService.GetProductsCountByCategoryAsync(catId);
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
            if (catId == null && branId == null && colorId == null && sizeId == null)
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
                    ProductImages = product.ProductImages,
                    Rating = product.Rating
                };
                mappedDatas.Add(productList);
            }
            return mappedDatas;
        }
    }
}
