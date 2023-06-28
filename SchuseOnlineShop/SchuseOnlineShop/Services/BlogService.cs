using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        public BlogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<Blog> GetByIdAsync(int? id)
        {
            return await _context.Blogs.FirstOrDefaultAsync(m=>m.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Blogs.CountAsync(); 
        }

        public async Task<List<Blog>> GetPaginatedDatasAsync(int page = 1, int take = 3)
        {
            return await _context.Blogs
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();
        }


    }
}
