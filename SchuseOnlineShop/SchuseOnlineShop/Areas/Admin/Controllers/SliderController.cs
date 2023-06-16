using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Areas.Admin.ViewModels.Slider;
using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ISliderService _sliderService;
        private readonly ICrudService<Slider> _crudService;


        public SliderController(IWebHostEnvironment env,
                                ISliderService sliderService,
                                ICrudService<Slider> crudService)
        {
            _env = env;
            _sliderService = sliderService;
            _crudService = crudService;

        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> dbSlider = await _sliderService.GetAllAsync();
            return View(dbSlider);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM model)
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

                Slider slider = new()
                {
                    Image = model.Photo.CreateFile(_env, "assets/img/home/slider"),
                    Title = model.Title,
                    Desc = model.Desc,
                    Heading = model.Heading
                };

                await _crudService.CreateAsync(slider);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                Slider dbSlider = await _sliderService.GetByIdAsync((int)id);
                if (dbSlider is null) return NotFound();

                SliderUpdateVM model = new()
                {
                    Image = dbSlider.Image,
                    Title = dbSlider.Title,
                    Desc = dbSlider.Desc,
                    Heading = dbSlider.Heading
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }


    }
}
