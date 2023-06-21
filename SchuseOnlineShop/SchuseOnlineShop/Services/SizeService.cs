using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Services
{
    public class SizeService : ISizeService
    {
        private readonly AppDbContext _context;
        public SizeService(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckByName(int number)
        {
            return _context.Sizes.Any(m => m.Number == number);

        }

        public async Task<IEnumerable<Size>> GetAllAsync()
        {
            return await _context.Sizes.ToListAsync();
        }

        public async Task<Size> GetByIdAsync(int? id)
        {
            return await _context.Sizes.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
