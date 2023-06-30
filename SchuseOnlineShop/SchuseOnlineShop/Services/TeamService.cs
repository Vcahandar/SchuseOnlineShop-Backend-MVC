using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;
        public TeamService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Team> GetByIdAsync(int? id)
        {
            return await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Team>> GetTeamsAll()
        {
            return await _context.Teams.ToListAsync();
        }
    }
}
