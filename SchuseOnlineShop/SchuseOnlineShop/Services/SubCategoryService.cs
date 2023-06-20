using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly AppDbContext _context;
        public SubCategoryService(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckByName(string name)
        {
            return _context.SubCategories.Any(m => m.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public async Task<List<SubCategory>> GetAllAsync()
        {
            return await _context.SubCategories.Include(m=>m.CategorySubCategories).ToListAsync();
        }

        public async Task<SubCategory> GetByIdAsync(int? id)
        {
            return await _context.SubCategories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
