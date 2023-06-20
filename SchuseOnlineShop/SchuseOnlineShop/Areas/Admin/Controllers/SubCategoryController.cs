using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchuseOnlineShop.Areas.Admin.ViewModels.Category;
using SchuseOnlineShop.Areas.Admin.ViewModels.SubCategory;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISubCategoryService _subCategory;
        private readonly ICrudService<SubCategory> _crudService;
        private readonly ICrudService<CategorySubCategory> _crudCateSub;
        private readonly ICategoryService _categoryService;

        public SubCategoryController(AppDbContext context,
                                     ISubCategoryService subCategory,
                                     ICrudService<SubCategory> crudService,
                                     ICategoryService categoryService,
                                     ICrudService<CategorySubCategory> crudCateSub)
        {
            _subCategory = subCategory;
            _context = context;
            _crudService = crudService;
            _categoryService = categoryService;
            _crudCateSub = crudCateSub;
        }


        public async Task<IActionResult>  Index()
        {
            IEnumerable<SubCategory> subCategories = await _subCategory.GetAllAsync();
            return View(subCategories);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await GetCategoryAsync();
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategoryCreateVM model)
        {
            try
            {
                ViewBag.categories = await GetCategoryAsync();
                SubCategory subCategory = new()
                {
                    Name = model.Name,
                };

                await _crudService.CreateAsync(subCategory);

                foreach (var cateId in model.CategoryIds)
                {
                    CategorySubCategory categorySub = new()
                    {
                        CategoryId = cateId,
                        SubCategoryId= subCategory.Id
                     };

                    await _crudCateSub.CreateAsync(categorySub);
                }
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
                ViewBag.categories = await GetCategoryAsync();
                if (id is null) return BadRequest();
                SubCategory dbSubCategory = await _subCategory.GetByIdAsync((int)id);
                if (dbSubCategory is null) return NotFound();

                SubCategoryUpdateVM model = new()
                {
                    Name = dbSubCategory.Name,
                    Id= dbSubCategory.Id
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
        public async Task<IActionResult> Edit(int? id, SubCategoryUpdateVM model)
        {
            try
            {
                ViewBag.categories = await GetCategoryAsync();
                if (!ModelState.IsValid) return View();

                if (id is null) return BadRequest();

                SubCategory dbSubCategory = await _subCategory.GetByIdAsync(id);

                if (dbSubCategory is null) return NotFound();

                if (dbSubCategory.Name.Trim().ToLower() == model.Name.Trim().ToLower())
                {
                    return RedirectToAction(nameof(Index));
                }

                SubCategory subCategory = new()
                {
                    Id = model.Id,
                    Name = model.Name
                };

                _crudService.Edit(subCategory);
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

                SubCategory dbSubCategory = await _subCategory.GetByIdAsync((int)id);

                if (dbSubCategory is null) return NotFound();

                _crudService.Delete(dbSubCategory);
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

            if (id == null) return BadRequest();

            SubCategory subCategory = await _subCategory.GetByIdAsync((int)id);

            if (subCategory is null) return NotFound();


            return View(subCategory);
        }


        





        private async Task<SelectList> GetCategoryAsync()
        {
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();

            return new SelectList(categories, "Id", "Name");
        }


    }
}
