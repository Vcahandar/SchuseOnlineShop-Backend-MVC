using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Areas.Admin.ViewModels.BrandLogo;
using SchuseOnlineShop.Areas.Admin.ViewModels.Slider;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandLogoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBrandLogoService _brandLogo;
        private readonly IWebHostEnvironment _env;
        private readonly ICrudService<BrandLogo> _crudService;
        public BrandLogoController(AppDbContext context, 
                                  IBrandLogoService brandLogo,
                                  IWebHostEnvironment env,
                                  ICrudService<BrandLogo> crudService)
        {
            _context = context;
            _brandLogo = brandLogo;
            _env = env;
            _crudService = crudService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<BrandLogo> brandLogos = await _brandLogo.GetBrandLogosAll();

            return View(brandLogos);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandLogoCreateVM model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                if (!model.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View();
                }
                if (!model.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 200kb");
                    return View();
                }

                BrandLogo brandLogo = new()
                {
                    Image = model.Photo.CreateFile(_env, "assets/img/logo"),
                
                };

                await _crudService.CreateAsync(brandLogo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
