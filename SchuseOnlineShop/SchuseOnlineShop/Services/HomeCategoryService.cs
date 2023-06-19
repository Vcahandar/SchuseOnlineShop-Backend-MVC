using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Services
{
    public class HomeCategoryService : IHomeCategoryService
    {
        readonly AppDbContext _context;
        public HomeCategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<HomeCategory>> GetAllAsync()
        {
            return await _context.HomeCategories.ToListAsync();
        }

        public async Task<HomeCategory> GetByIdAsync(int id)
        {
            return await _context.HomeCategories.FirstOrDefaultAsync(m => m.Id == id);
        }


    }
}
