using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Areas.Admin.ViewModels.BrandLogo;
using SchuseOnlineShop.Areas.Admin.ViewModels.Slider;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services;
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
                    Image = model.Photo.CreateFile(_env, "assets/img/home/brandlogo"),
                
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



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                BrandLogo dbBrandLogo = await _context.BrandLogos.FirstOrDefaultAsync(m=>m.Id== id);
                if (dbBrandLogo is null) return NotFound();

                BrandLogoUpdateVM model = new()
                {
                    Image = dbBrandLogo.Image,
                  
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
        public async Task<IActionResult> Edit(int? id, BrandLogoUpdateVM model)
        {
            try
            {
                if (id is null) return BadRequest();
                BrandLogo dbBrandLogo = await _brandLogo.GetByIdAsync((int)id);
                if (dbBrandLogo is null) return NotFound();

                BrandLogoUpdateVM brandLogoUpdateVM = new()
                {
                    Image = dbBrandLogo.Image
                };

                if (!ModelState.IsValid) return View(brandLogoUpdateVM);

                if (model.Photo is not null)
                {
                    if (!model.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(brandLogoUpdateVM);
                    }
                    if (!model.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(brandLogoUpdateVM);
                    }
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/home/brandlogo", brandLogoUpdateVM.Image);
                    FileHelper.DeleteFile(path);

                    dbBrandLogo.Image = model.Photo.CreateFile(_env, "assets/img/home/brandlogo");
                }
                else
                {
                    Slider newSlider = new()
                    {
                        Image = dbBrandLogo.Image
                    };
                }

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
                BrandLogo dbBrandLogo = await _brandLogo.GetByIdAsync((int)id);
                if (dbBrandLogo is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/home/brandlogo", dbBrandLogo.Image);
                FileHelper.DeleteFile(path);

                _crudService.Delete(dbBrandLogo);
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
