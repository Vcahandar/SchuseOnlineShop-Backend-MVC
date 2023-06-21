using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Services
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;
        public BrandService(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckByName(string name)
        {
           return _context.Brands.Any(m => m.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<Brand> GetByIdAsync(int? id)
        {
            return await _context.Brands.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Brands.CountAsync();
        }

        public async Task<List<Brand>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.Brands.Skip((page * take)-take).Take(take).ToListAsync();
        }
    }
}
