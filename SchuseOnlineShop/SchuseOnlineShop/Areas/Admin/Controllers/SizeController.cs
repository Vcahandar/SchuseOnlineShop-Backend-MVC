using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Areas.Admin.ViewModels.Size;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin,Admin")]
	public class SizeController : Controller
    {
        private readonly ISizeService _sizeService;
        private readonly ICrudService<Size> _crudService;
        public SizeController(ISizeService sizeService, 
                              ICrudService<Size> crudService)
        {
            _sizeService = sizeService;
            _crudService = crudService;
        }

        public async Task<IActionResult>  Index()
        {
            return View(await _sizeService.GetAllAsync());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SizeVM model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                if (_sizeService.CheckByName(model.Number))
                {
                    ModelState.AddModelError("Number", "Number already exist");
                    return View(model);
                }

                Size size = new()
                {
                    Number = model.Number
                };

                await _crudService.CreateAsync(size);
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

                Size dbSize = await _sizeService.GetByIdAsync((int)id);

                if (dbSize is null) return NotFound();

                _crudService.Delete(dbSize);

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

