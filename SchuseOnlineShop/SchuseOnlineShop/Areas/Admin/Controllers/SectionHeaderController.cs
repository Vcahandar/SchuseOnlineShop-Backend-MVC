using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Services.Interfaces;
using SchuseOnlineShop.Models;
using Microsoft.AspNetCore.Authorization;

namespace SchuseOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin,Admin")]
	public class SectionHeaderController : Controller
    {
        private readonly ICrudService<SectionHeader> _crudService;
        private readonly ILayoutService _layoutService;
        private readonly IWebHostEnvironment _env;
        public SectionHeaderController(ICrudService<SectionHeader> crudService, 
                                       ILayoutService layoutService,
                                       IWebHostEnvironment env)
        {
            _crudService = crudService;
            _layoutService = layoutService;
            _env = env;
        }

        public async Task<IActionResult>  Index()
        {
            return View(await _layoutService.GetSectionsDatasAsync());
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return View(await _layoutService.GetSectionAsync((int)id));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int? id, Setting setting)
        {
            try
            {
                var dbSectionHeader = await _layoutService.GetSectionAsync((int)id);

                if (dbSectionHeader == null) return View();

                if (dbSectionHeader.Value.Trim().ToLower() == setting.Value.Trim().ToLower())
                {
                    return RedirectToAction(nameof(Index));
                }

                dbSectionHeader.Value = setting.Value;

                await _crudService.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            return View(await _layoutService.GetSectionAsync((int)id));
        }
    }
}
