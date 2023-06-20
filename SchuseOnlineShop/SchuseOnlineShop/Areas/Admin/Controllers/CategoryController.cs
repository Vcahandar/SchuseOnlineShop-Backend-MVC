using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Areas.Admin.ViewModels.Blog;
using SchuseOnlineShop.Areas.Admin.ViewModels.Category;
using SchuseOnlineShop.Areas.Admin.ViewModels.Slider;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICategoryService _category;
        private readonly ICrudService<Category> _crudService;

        public CategoryController(AppDbContext context, 
                                    ICategoryService category, 
                                    ICrudService<Category> crudService)
        {
            _context = context;
            _category = category;
            _crudService = crudService;
        }

        public async Task<IActionResult>  Index()
        {
            IEnumerable<Category> categories = await _category.GetAllAsync();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM model)
        {
            try
            {
                Category category = new()
                {
                    Name = model.Name,
                };

                await _crudService.CreateAsync(category);
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
                Category dbCategory = await _category.GetByIdAsync((int)id);
                if (dbCategory is null) return NotFound();

                CategoryUpdateVM model = new()
                {
                    Name = dbCategory.Name,
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
        public async Task<IActionResult> Edit(int? id,CategoryUpdateVM model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                if (id is null) return BadRequest();

                Category dbCategory = await _category.GetByIdAsync(id);

                if (dbCategory is null) return NotFound();

                if (dbCategory.Name.Trim().ToLower() == model.Name.Trim().ToLower())
                {
                    return RedirectToAction(nameof(Index));
                }

                Category category = new()
                {
                    Id = model.Id,
                    Name = model.Name
                };

                _crudService.Edit(category);
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

                Category dbCategory = await _category.GetByIdAsync((int)id);

                if (dbCategory is null) return NotFound();

                _crudService.Delete(dbCategory);
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

