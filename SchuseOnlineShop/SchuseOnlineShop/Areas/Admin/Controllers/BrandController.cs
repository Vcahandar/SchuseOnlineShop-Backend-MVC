using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Areas.Admin.ViewModels.Brand;
using SchuseOnlineShop.Areas.Admin.ViewModels.SubCategory;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services;
using SchuseOnlineShop.Services.Interfaces;
using System.Data;

namespace SchuseOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBrandService _brandService;
        private readonly ICrudService<Brand> _crudService;

        public BrandController(AppDbContext context,
                               ICrudService<Brand> crudService,
                               IBrandService brandService)
        {
            _context = context;
            _brandService = brandService;
            _crudService= crudService;
        }


        public async Task<IActionResult>  Index(int page =1,int take =5)
        {
            List<Brand> brands = await _brandService.GetPaginatedDatasAsync(page, take);
            List<BrandVM> mappedDatas = GetDatas(brands);

            int pageCount = await GetPageCountAsync(take);

            Paginate<BrandVM> paginatedDatas = new(mappedDatas, page, pageCount);

            //IEnumerable<Brand> brands = await _brandService.GetAllAsync();
            return View(paginatedDatas);
        }


        private async Task<int> GetPageCountAsync(int take)
        {
            var brandCount = await _brandService.GetCountAsync();
            return (int)Math.Ceiling((decimal)brandCount / take);
        }

        private List<BrandVM> GetDatas(List<Brand> brands)
        {
            List<BrandVM> mappedDatas = new();
            foreach (var brand in brands)
            {
                BrandVM brandList = new()
                {
                    Id = brand.Id,
                    Name = brand.Name,
                };
                mappedDatas.Add(brandList);
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
        public async Task<IActionResult> Create(BrandVM model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                if (_brandService.CheckByName(model.Name))
                {
                    ModelState.AddModelError("Name", "Name already exist");
                    return View(model);
                }

                Brand brand = new() { Name = model.Name };

                await _crudService.CreateAsync(brand);
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
            Brand dbBrand = await _brandService.GetByIdAsync((int)id);
            if (dbBrand is null) return NotFound();

            BrandVM brand = new()
            {
                Id = dbBrand.Id,
                Name = dbBrand.Name,
            };
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BrandVM model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                if (id is null) return BadRequest();

                Brand dbBrand = await _brandService.GetByIdAsync((int)id);

                if (dbBrand is null) return NotFound();

                if (dbBrand.Name.Trim().ToLower() == model.Name.Trim().ToLower())
                {
                    return RedirectToAction(nameof(Index));
                }

                Brand brand = new()
                {
                    Id = model.Id,
                    Name = model.Name
                };

                _crudService.Edit(brand);
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

                Brand dbBrand = await _brandService.GetByIdAsync((int)id);

                if (dbBrand is null) return NotFound();

                _crudService.Delete(dbBrand);

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
