using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Services
{
    public class ColorService : IColorService
    {
        private readonly AppDbContext _context;
        public ColorService(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckByName(string name)
        {
            return _context.Colors.Any(m => m.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public async Task<IEnumerable<Color>> GetAllAsync()
        {
            return await _context.Colors.ToListAsync();
        }

        public async Task<Color> GetByIdAsync(int? id)
        {
            return await _context.Colors.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public  async Task<int> GetCountAsync()
        {
           return await _context.Colors.CountAsync();
        }

        public async Task<List<Color>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.Colors.Skip((page * take) - take).Take(take).ToListAsync();
        }
    }
}
