using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Services;
using SchuseOnlineShop.Services.Interfaces;
using SchuseOnlineShop.ViewModels.About;

namespace SchuseOnlineShop.Controllers
{
    public class AboutController : Controller
    {
        private readonly ITeamService _teamService;
        public AboutController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
        {
            AboutVM model = new()
            {
                Teams = await _teamService.GetTeamsAll(),
            };
            return View(model);
        }
    }
}
