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
            return _context.SubCategories.Any(c => c.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public Task<List<SubCategory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SubCategory> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
