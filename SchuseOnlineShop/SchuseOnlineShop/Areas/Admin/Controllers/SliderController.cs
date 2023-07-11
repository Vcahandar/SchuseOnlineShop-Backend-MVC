using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Areas.Admin.ViewModels.Slider;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin,Admin")]
	public class SliderController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ISliderService _sliderService;
        private readonly ICrudService<Slider> _crudService;
        private readonly AppDbContext _context;



        public SliderController(IWebHostEnvironment env,
                                ISliderService sliderService,
                                ICrudService<Slider> crudService,
                                AppDbContext context)
        {
            _env = env;
            _sliderService = sliderService;
            _crudService = crudService;
            _context = context;

        }



        public async Task<IActionResult> Index(string viewName, string controllerName)
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderUpdateVM model)
        {
            try
            {
                if (id is null) return BadRequest();
                Slider dbSlider = await _sliderService.GetByIdAsync((int)id);
                if (dbSlider is null) return NotFound();

                SliderUpdateVM sliderUpdateVM = new()
                {
                    Image = dbSlider.Image
                };

                if (!ModelState.IsValid) return View(sliderUpdateVM);

                if (model.Photo is not null)
                {
                    if (!model.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(sliderUpdateVM);
                    }
                    if (!model.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(sliderUpdateVM);
                    }
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/home/slider", sliderUpdateVM.Image);
                    FileHelper.DeleteFile(path);

                    dbSlider.Image = model.Photo.CreateFile(_env, "assets/img/home/slider");
                }
                else
                {
                    Slider newSlider = new()
                    {
                        Image = dbSlider.Image
                    };
                }

                dbSlider.Title = model.Title;
                dbSlider.Heading = model.Heading;
                dbSlider.Desc = model.Desc;
                await _crudService.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                Slider dbSlider = await _sliderService.GetByIdAsync((int)id);
                if (dbSlider is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/home/slider", dbSlider.Image);
                FileHelper.DeleteFile(path);

                _crudService.Delete(dbSlider);
                return Ok();
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
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
