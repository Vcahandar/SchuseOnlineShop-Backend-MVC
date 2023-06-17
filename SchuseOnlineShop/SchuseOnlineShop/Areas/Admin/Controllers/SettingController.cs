using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ICrudService<Slider> _crudService;
        private readonly ILayoutService _layoutService;

        public SettingController(IWebHostEnvironment env, 
                                 ICrudService<Slider> crudService,
                                 ILayoutService layoutService)
        {
            _env = env;
            _crudService = crudService;
            _layoutService = layoutService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _layoutService.GetSettingDatas());
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var dbSsetting = _layoutService.GetById((int)id);

            return View(dbSsetting);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Setting setting)
        {
            try
            {
                var dbSetting = _layoutService.GetById((int)id);

                if (dbSetting == null) return View();


                if (dbSetting.Value.Contains(".web") || dbSetting.Value.Contains(".jpg") || dbSetting.Value.Contains(".jpeg") || dbSetting.Value.Contains(".ong"))
                {
                    if (setting.CompanyLogoPhoto is not null)
                    {
                        if (!setting.CompanyLogoPhoto.CheckFileType("image/"))
                        {
                            ModelState.AddModelError("CompanyLogoPhoto", "File type must be image");
                            return View();
                        }
                        if (!setting.CompanyLogoPhoto.CheckFileSize(200))
                        {
                            ModelState.AddModelError("CompanyLogoPhoto", "Image size must be max 200kb");
                            return View();
                        }
                        string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/logo", dbSetting.Value);
                        FileHelper.DeleteFile(path);

                        dbSetting.Value = setting.CompanyLogoPhoto.CreateFile(_env, "assets/img/logo");
                    }
                    else
                    {
                        Setting newSetting = new()
                        {
                            Value = dbSetting.Value
                        };
                    }
                }
                else
                {

                    if (dbSetting.Value.Trim().ToLower() == setting.Value.Trim().ToLower())
                    {

                        return RedirectToAction(nameof(Index));
                    }
                    dbSetting.Value = setting.Value;

                }

                await _crudService.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        public IActionResult Detail(int? id)
        {
            var dbSsetting = _layoutService.GetById((int)id);

            return View(dbSsetting);
        }
    }
}
