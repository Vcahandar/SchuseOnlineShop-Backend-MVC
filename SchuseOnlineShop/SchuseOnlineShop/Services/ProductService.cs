using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;
using SchuseOnlineShop.ViewModels.Product;
using System.Linq;

namespace SchuseOnlineShop.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(p => p.ProductImages).ToListAsync();
        }



        public async Task<Product> GetByIdAsync(int? id)
        {
            return await _context.Products
                  .Include(m => m.CategorySubCategories)
                  .ThenInclude(m => m.Category).Include(m=>m.CategorySubCategories).ThenInclude(m=>m.SubCategory)
                  .Include(m => m.ProductImages)
                  .Include(m => m.ProductVideos)
                  .Include(m => m.ProductColors)
                  .Include(m => m.ProductSizes)
                  .FirstOrDefaultAsync(m => m.Id == id);
        }



        public async Task<List<Product>> GetFullDataAsync()
        {
            return await _context.Products
                 .Include(m => m.ProductVideos)
                 .Include(m => m.ProductImages)
                 .Include(m => m.ProductComments)
                 .Include(m => m.CategorySubCategories)
                 .ThenInclude(m => m.Category)
                 .Include(m => m.CategorySubCategories)
                 .ThenInclude(m => m.SubCategory)
                 .Include(m => m.ProductColors)
                 .ThenInclude(m => m.Color)
                 .Include(m => m.ProductSizes)
                 .ThenInclude(m => m.Size)
                 .Include(m => m.Brand)
                 .ToListAsync();


        }


        public async Task<Product> GetFullDataByIdAsync(int? id)
        {
            return await _context.Products
              .Include(m => m.ProductVideos)
              .Include(m => m.ProductImages)
              .Include(m => m.ProductComments)
              .Include(m => m.CategorySubCategories)
              .ThenInclude(m => m.Category)
              .Include(m => m.CategorySubCategories)
              .ThenInclude(m => m.SubCategory)
              .Include(m => m.ProductColors)
              .ThenInclude(m => m.Color)
              .Include(m => m.ProductSizes)
              .ThenInclude(m => m.Size)
              .Include(m => m.Brand)
              .FirstOrDefaultAsync(m => m.Id == id);
        }



        public async Task<ProductImage> GetImageById(int? id)
        {
            return await _context.ProductImages.FindAsync((int)id);
        }



        public async Task<Product> GetProductByImageId(int? id)
        {
            return await _context.Products
             .Include(m => m.ProductImages)
             .FirstOrDefaultAsync(m => m.ProductImages.Any(m => m.Id == id));
        }



        public async Task<ProductVideo> GetVideoById(int? id)
        {
            return await _context.ProductVideos.FindAsync((int)id);
        }



        public async Task<Product> GetProductByVideoId(int? id)
        {
            return await _context.Products
           .Include(m => m.ProductVideos)
           .FirstOrDefaultAsync(m => m.ProductVideos.Any(m => m.Id == id));
        }


        public void RemoveImage(ProductImage image)
        {
            _context.Remove(image);
        }

        public void RemoveVideo(ProductVideo video)
        {
            _context.Remove(video);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<List<Product>> GetPaginatedDatasAsync(int page, int take, int? categoryId, int? colorId, int? brandId, int? sizeId)
        {


            throw new NotImplementedException();
            //if (categoryId != null)
            //{
            //    return await _context.CategorySubCategories
            //    .Include(m => m.SubCategory)
            //    .ThenInclude(m=>m.CategorySubCategories)
            //    .Include(m=>m.Category)
            //    .Where(m => m.CategoryId == categoryId)
            //    .Select(m=>m)
            //    .Skip((page * take) - take)
            //    .Take(take)
            //    .ToListAsync();
            //}
        }

        public Task<List<ProductVM>> GetProductsByCategoryIdAsync(int? id, int page, int take)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductVM>> GetProductsByColorIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductVM>> GetProductsByTagIdAsync(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
