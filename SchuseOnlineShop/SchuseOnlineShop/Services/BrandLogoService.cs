using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Services
{
    public class BrandLogoService : IBrandLogoService
    {
        private readonly AppDbContext _context;
        public BrandLogoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BrandLogo>> GetBrandLogosAll()
        {
            return await _context.BrandLogos.ToListAsync();
        }

        public async Task<BrandLogo> GetByIdAsync(int? id)
        {
            return await _context.BrandLogos.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
