using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Areas.Admin.ViewModels.Brand;
using SchuseOnlineShop.Areas.Admin.ViewModels.Color;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin,Admin")]
	public class ColorController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IColorService _colorService;
        private readonly ICrudService<Color> _crudService;

        public ColorController(AppDbContext context, 
                                IColorService colorService,
                                ICrudService<Color> crudService)
        {
            _context = context;
            _colorService = colorService;
            _crudService = crudService;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            List<Color> colors = await _colorService.GetPaginatedDatasAsync(page, take);
            List<ColorVM> mappedDatas = GetDatas(colors);

            int pageCount = await GetPageCountAsync(take);

            Paginate<ColorVM> paginatedDatas = new(mappedDatas, page, pageCount);

            //IEnumerable<Brand> brands = await _brandService.GetAllAsync();
            return View(paginatedDatas);
        }


        private async Task<int> GetPageCountAsync(int take)
        {
            var colorCount = await _colorService.GetCountAsync();
            return (int)Math.Ceiling((decimal)colorCount / take);
        }
        private List<ColorVM> GetDatas(List<Color> colors)
        {
            List<ColorVM> mappedDatas = new();
            foreach (var color in colors)
            {
                ColorVM colorList = new()
                {
                    Id = color.Id,
                    Name = color.Name,
                };
                mappedDatas.Add(colorList);
            }
            return mappedDatas;
        }




        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ColorVM model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                if (_colorService.CheckByName(model.Name))
                {
                    ModelState.AddModelError("Name", "Name already exist");
                    return View(model);
                }

                Color color = new() { Name = model.Name };

                await _crudService.CreateAsync(color);
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
            if (id is null) return BadRequest();
            Color dbColor = await _colorService.GetByIdAsync((int)id);
            if (dbColor is null) return NotFound();

            ColorVM color = new()
            {
                Id = dbColor.Id,
                Name = dbColor.Name,
            };
            return View(color);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ColorVM model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                if (id is null) return BadRequest();

                Color dbColor = await _colorService.GetByIdAsync((int)id);

                if (dbColor is null) return NotFound();

                if (dbColor.Name.Trim().ToLower() == model.Name.Trim().ToLower())
                {
                    return RedirectToAction(nameof(Index));
                }

                Color color = new()
                {
                    Id = model.Id,
                    Name = model.Name
                };

                _crudService.Edit(color);
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

                Color dbColor = await _colorService.GetByIdAsync((int)id);

                if (dbColor is null) return NotFound();

                _crudService.Delete(dbColor);

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
