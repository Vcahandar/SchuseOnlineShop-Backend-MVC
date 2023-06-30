using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Areas.Admin.ViewModels.Blog;
using SchuseOnlineShop.Areas.Admin.ViewModels.Team;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Helpers;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITeamService _teamService;
        private readonly IWebHostEnvironment _env;
        private readonly ICrudService<Team> _crudService;


        public TeamController(AppDbContext context, ITeamService teamService, IWebHostEnvironment env, ICrudService<Team> crudService)
        {
            _context = context;
            _teamService = teamService;
            _env = env;
            _crudService = crudService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Team> teams = await _teamService.GetTeamsAll();
            return View(teams);
        }





        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateVM model)
        {
            try
            {

                if (!ModelState.IsValid) return View();


                if (!model.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View();
                }
                if (!model.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "Image size must be max 500kb");
                    return View();
                }



                Team newTeam = new()
                {
                    FullName = model.FullName,
                    Position = model.Position,
                    Image = model.Photo.CreateFile(_env, "assets/img/team")

                };


                await _context.Teams.AddAsync(newTeam);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return BadRequest();

                Team team = await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);

                if (team is null) return NotFound();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/img/team", team.Image);

                FileHelper.DeleteFile(path);

                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                throw;
            }

        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id is null) return BadRequest();
                Team dbTeam = await _teamService.GetByIdAsync((int)id);
                if (dbTeam is null) return NotFound();

                TeamUpdateVM model = new()
                {
                    Image = dbTeam.Image,
                    FullName = dbTeam.FullName,
                    Position = dbTeam.Position,
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
        public async Task<IActionResult> Edit(int? id, TeamUpdateVM model)
        {
            try
            {
                if (id is null) return BadRequest();
                Team dbTeam = await _teamService.GetByIdAsync((int)id);
                if (dbTeam is null) return NotFound();

                TeamUpdateVM teamUpdateVM = new()
                {
                    Image = dbTeam.Image
                };

                if (!ModelState.IsValid) return View(teamUpdateVM);

                if (model.Photo is not null)
                {
                    if (!model.Photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(teamUpdateVM);
                    }
                    if (!model.Photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(teamUpdateVM);
                    }
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/team", teamUpdateVM.Image);
                    FileHelper.DeleteFile(path);

                    dbTeam.Image = model.Photo.CreateFile(_env, "assets/img/team");
                }
                else
                {
                    Team newTeam = new()
                    {
                        Image = dbTeam.Image
                    };
                }

                dbTeam.FullName = model.FullName;
                dbTeam.Position = model.Position;
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
                Team team = await _teamService.GetByIdAsync(id);
                if (team is null) return NotFound();
                return View(team);
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }



    }
}
