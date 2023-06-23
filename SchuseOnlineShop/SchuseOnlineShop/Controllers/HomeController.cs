using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;
using SchuseOnlineShop.ViewModels.Home;
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

        public async Task<IActionResult>  Index(int? id)
        {
           
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



    }
}