using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        public UserService(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task DeleteAsync(AppUser user)
        {
            await _userManager.DeleteAsync(user);
        }

        public async Task<List<AppUser>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<AppUser> GetByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.AppUsers.CountAsync();
        }

        public async Task<List<AppUser>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.AppUsers.Skip((page * take) - take).Take(take).ToListAsync();

        }
    }
}
