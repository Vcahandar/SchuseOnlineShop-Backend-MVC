using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;
using SchuseOnlineShop.Areas.Admin.ViewModels.Blog;
using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Areas.Admin.ViewModels.Slider;
using SchuseOnlineShop.Services;
using Microsoft.EntityFrameworkCore;

namespace SchuseOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class BlogController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IBlogService _blogService;
        private readonly ICrudService<Blog> _crudService;
        private readonly AppDbContext _context;

        public BlogController(IWebHostEnvironment env, 
                              IBlogService blogService, 
                               ICrudService<Blog> crudService,
                               AppDbContext context)
        {
            _env = env;
            _blogService = blogService;
            _crudService = crudService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> dbBlog = await _blogService.GetAllAsync();
            return View(dbBlog);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM model)
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

                Blog blog = new()
                {
                    Image = model.Photo.CreateFile(_env, "assets/img/home/blog"),
                    Title = model.Title,
                    Description = model.Description,
                };

                await _crudService.CreateAsync(blog);
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
                Blog dbBlog = await _blogService.GetByIdAsync((int)id);
                if (dbBlog is null) return NotFound();

                BlogUpdateVM model = new()
                {
                    Image = dbBlog.Image,
                    Title = dbBlog.Title,
                    Description = dbBlog.Description,
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
        public async Task<IActionResult> Edit(int? id, BlogUpdateVM model)
        {
            try
            {
                if (id is null) return BadRequest();
                Blog dbBlog = await _blogService.GetByIdAsync((int)id);
                if (dbBlog is null) return NotFound();

                BlogUpdateVM blogUpdateVM = new()
                {
                    Image = dbBlog.Image
                };

                if (!ModelState.IsValid) return View(blogUpdateVM);

                if (model.Photo is not null)
                {
                    if (!model.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(blogUpdateVM);
                    }
                    if (!model.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(blogUpdateVM);
                    }
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/home/blog", blogUpdateVM.Image);
                    FileHelper.DeleteFile(path);

                    dbBlog.Image = model.Photo.CreateFile(_env, "assets/img/home/blog");
                }
                else
                {
                    Slider newSlider = new()
                    {
                        Image = dbBlog.Image
                    };
                }

                dbBlog.Title = model.Title;
                dbBlog.Description = model.Description;
                await _crudService.SaveAsync();
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
                Blog blog = await _blogService.GetByIdAsync(id);
                if (blog is null) return NotFound();
                return View(blog);
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
                Blog dbBlog = await _blogService.GetByIdAsync((int)id);
                if (dbBlog is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/home/blog", dbBlog.Image);
                FileHelper.DeleteFile(path);

                _crudService.Delete(dbBlog);
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
