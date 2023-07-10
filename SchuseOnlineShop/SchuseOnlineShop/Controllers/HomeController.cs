using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;
using SchuseOnlineShop.ViewModels.Home;
using SchuseOnlineShop.ViewModels.Product;
using System.Diagnostics;

namespace SchuseOnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IBrandLogoService _brandLogoService;
        private readonly IBlogService _blogService;
        private readonly IHomeCategoryService _homeCategory;
        private readonly IProductService _productService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly IBrandService _brandService;
        public HomeController(ISliderService sliderService,
                              IBrandLogoService brandLogo,
                              IBlogService blogService,
                              IHomeCategoryService homeCategory,
                              IProductService productService,
                              ISubCategoryService subCategoryService,
                              IBrandService brandService)
        {
            _sliderService = sliderService;
            _brandLogoService = brandLogo;
            _blogService = blogService;
            _homeCategory = homeCategory;
            _productService = productService;
            _subCategoryService = subCategoryService;
            _brandService = brandService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            //Product dbProduct = new();
            //ModalVM modal = new();
            //if (id != null)
            //{
            //    dbProduct = await _productService.GetDatasModalProductByIdAsyc((int)id);
            //    if (dbProduct is null) return NotFound();
            //     modal = new()
            //    {
            //        Id = dbProduct.Id,
            //        Name = dbProduct.Name,
            //        Price = dbProduct.Price,
            //        Description = dbProduct.Description,
            //        Brand = dbProduct.Brand.Name,
            //        Sku = dbProduct.SKU,
            //        Category = dbProduct.Category.Name,
            //        ProductImages = dbProduct.ProductImages,
            //    };
            //}

            HomeVM model = new()
            {
                Sliders = await _sliderService.GetAllAsync(),
                BrandLogos = await _brandLogoService.GetBrandLogosAll(),
                Blogs = await _blogService.GetAllAsync(),
                HomeCategories = await _homeCategory.GetAllAsync(),
                Products = await _productService.GetAllAsync(),
            };

            return View(model);
        }

        public async Task<IActionResult> GetDataProductModal(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                var dbProduct = await _productService.GetDatasModalProductByIdAsyc((int)id);
                if (dbProduct is null) return NotFound();
                var brandName = dbProduct.Brand.Name;
                var cateName = dbProduct.Category.Name;
                var mainImage = dbProduct.ProductImages.Where(p => p.IsMain).FirstOrDefault().ImgName;
                ModalVM model = new()
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Price = dbProduct.Price,
                    DiscountPrice = dbProduct.DiscountPrice,
                    Description = dbProduct.Description,
                    BrandName = brandName,
                    Sku = dbProduct.SKU,
                    CategoryName = cateName,
                    Image = mainImage,

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