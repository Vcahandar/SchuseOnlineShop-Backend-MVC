using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Areas.Admin.ViewModels.Blog;
using SchuseOnlineShop.Areas.Admin.ViewModels.HomeCategory;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeCategoryController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHomeCategoryService _homeCategory;
        private readonly ICrudService<HomeCategory> _crudService;
        private readonly AppDbContext _context;

        public HomeCategoryController(IWebHostEnvironment env,
                                        IHomeCategoryService homeCategory,
                                        ICrudService<HomeCategory> crudService,
                                        AppDbContext context)
        {
            _env = env;
            _homeCategory = homeCategory;
            _crudService = crudService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<HomeCategory> dbHomeCategory = await _homeCategory.GetAllAsync();
            return View(dbHomeCategory);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HomeCategoryCreateVM model)
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

                HomeCategory homeCategory = new()
                {
                    Image = model.Photo.CreateFile(_env, "assets/img/home/categoryImg"),
                    Name = model.Name,
                };

                await _crudService.CreateAsync(homeCategory);
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
                HomeCategory dbHomeCategory = await _homeCategory.GetByIdAsync((int)id);
                if (dbHomeCategory is null) return NotFound();

                HomeCategoryUpdateVM model = new()
                {
                    Image = dbHomeCategory.Image,
                    Name = dbHomeCategory.Name,
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
        public async Task<IActionResult> Edit(int? id, HomeCategoryUpdateVM model)
        {
            try
            {
                if (id is null) return BadRequest();
                HomeCategory dbHomeCategory = await _homeCategory.GetByIdAsync((int)id);
                if (dbHomeCategory is null) return NotFound();

                HomeCategoryUpdateVM homeCategoryUpdateVM = new()
                {
                    Image = dbHomeCategory.Image,
                    Name = dbHomeCategory.Name
                    
                };

                if (!ModelState.IsValid) return View(homeCategoryUpdateVM);

                if (model.Photo is not null)
                {
                    if (!model.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(homeCategoryUpdateVM);
                    }
                    if (!model.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(homeCategoryUpdateVM);
                    }
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/home/categoryImg", homeCategoryUpdateVM.Image);
                    FileHelper.DeleteFile(path);

                    dbHomeCategory.Image = model.Photo.CreateFile(_env, "assets/img/home/categoryImg");
                }
                else
                {
                    HomeCategory newHomeCategory = new()
                    {
                        Image = dbHomeCategory.Image
                    };
                }

                dbHomeCategory.Name = model.Name;
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
                HomeCategory dbHomeCategory = await _homeCategory.GetByIdAsync((int)id);
                if (dbHomeCategory is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/home/categoryImg", dbHomeCategory.Image);
                FileHelper.DeleteFile(path);

                _crudService.Delete(dbHomeCategory);
                return RedirectToAction(nameof(Index));
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
                if (id == null) return BadRequest();
                HomeCategory homeCategory = await _homeCategory.GetByIdAsync((int)id);
                if (homeCategory is null) return NotFound();
                return View(homeCategory);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }

    }
}
