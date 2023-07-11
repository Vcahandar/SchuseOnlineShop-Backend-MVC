using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Areas.Admin.ViewModels.Advert;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin,Admin")]
	public class AdvertController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ICrudService<Advert> _crudService;
        private readonly IAdvertService _advertService;
        public AdvertController(IWebHostEnvironment env, IAdvertService advertService,
           AppDbContext context, ICrudService<Advert> crudService)
        {
            _env = env;
            _advertService = advertService;
            _crudService = crudService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _advertService.GetAllAsync());
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdvertCreateVM model)
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

                Advert advert = new()
                {
                    Image = model.Photo.CreateFile(_env, "assets/img/about"),
                    ShortDesc = model.ShortDesc,
                    Title = model.Title
                };

                await _crudService.CreateAsync(advert);
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
                Advert dbAdvert = await _advertService.GetByIdAsync((int)id);
                if (dbAdvert is null) return NotFound();

                AdvertUpdateVM model = new()
                {
                    Image = dbAdvert.Image,
                    Title = dbAdvert.Title,
                    ShortDesc = dbAdvert.ShortDesc,
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
        public async Task<IActionResult> Edit(int? id, AdvertUpdateVM model)
        {
            try
            {
                if (id is null) return BadRequest();
                Advert dbAdvert = await _advertService.GetByIdAsync((int)id);
                if (dbAdvert is null) return NotFound();

                AdvertUpdateVM advertUpdateVM = new()
                {
                    Image = dbAdvert.Image
                };

                if (!ModelState.IsValid) return View(advertUpdateVM);

                if (model.Photo is not null)
                {
                    if (!model.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(advertUpdateVM);
                    }
                    if (!model.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(advertUpdateVM);
                    }
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/about", advertUpdateVM.Image);
                    FileHelper.DeleteFile(path);

                    dbAdvert.Image = model.Photo.CreateFile(_env, "assets/img/about");
                }
                else
                {
                    Advert newAdvert = new()
                    {
                        Image = dbAdvert.Image
                    };
                }

                dbAdvert.Title = model.Title;
                dbAdvert.ShortDesc = model.ShortDesc;
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
                Advert dbAdvert = await _advertService.GetByIdAsync((int)id);
                if (dbAdvert is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/about", dbAdvert.Image);
                FileHelper.DeleteFile(path);

                _crudService.Delete(dbAdvert);
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
