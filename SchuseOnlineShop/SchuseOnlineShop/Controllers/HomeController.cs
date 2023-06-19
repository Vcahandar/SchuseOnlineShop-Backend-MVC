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
        public HomeController(ISliderService sliderService,
                              IBrandLogoService brandLogo,
                              IBlogService blogService,
                              IHomeCategoryService homeCategory)
        {
            _sliderService = sliderService;
            _brandLogoService = brandLogo;
            _blogService = blogService;
            _homeCategory = homeCategory;
        }

        public async Task<IActionResult>  Index()
        {
            HomeVM model = new()
            {
                Sliders = await _sliderService.GetAllAsync(),
                BrandLogos = await _brandLogoService.GetBrandLogosAll(),
                Blogs = await _blogService.GetAllAsync(),
                HomeCategories = await _homeCategory.GetAllAsync()
            };

            return View(model);
        }



    }
}