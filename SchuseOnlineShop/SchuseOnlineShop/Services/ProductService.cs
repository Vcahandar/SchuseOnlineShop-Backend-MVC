    using Microsoft.AspNetCore.Mvc.RazorPages;
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
                  .Include(m => m.Category)
                  .Include(m => m.SubCategory)
                  .Include(m => m.ProductImages)
                  .Include(m => m.ProductColors)
                  .ThenInclude(m => m.Color)
                  .Include(m => m.ProductSizes)
                  .FirstOrDefaultAsync(m => m.Id == id);
        }



        public async Task<List<Product>> GetFullDataAsync()
        {
            return await _context.Products
                 .Include(m => m.ProductImages)
                 .Include(m => m.ProductComments)
                   .Include(m => m.Category)
                  .Include(m => m.SubCategory)
                 .Include(m => m.ProductColors)
                 .ThenInclude(m => m.Color)
                 .Include(m => m.ProductSizes)
                 .ThenInclude(m => m.Size)
                 .Include(m => m.Brand)
                 .ToListAsync();


        }


        public async Task<Product> GetFullDataByIdAsync(int? id)
        {
            return await _context.Products.Include(m => m.ProductImages)
              .Include(m => m.ProductComments)
              .Include(m => m.Category)
              .Include(m => m.SubCategory)
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



        public void RemoveImage(ProductImage image)
        {
            _context.Remove(image);
        }




        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }


        public async Task<List<Product>> GetPaginatedDatasAsync(int page, int take, int? categoryId, int? subCategoryId, int? colorId, int? brandId, int? sizeId)
        {
            List<Product> products = new List<Product>();

            if(categoryId == null || subCategoryId == null || colorId == null || brandId == null || sizeId == null )
            {
                products = await _context.Products
                    .Include(m => m.SubCategory)
                    .Include(m => m.Category)
                    .Include(m => m.ProductImages)
                    .Include(m => m.ProductColors)
                    .ThenInclude(m => m.Color)
                    .Include(m => m.ProductSizes)
                    .ThenInclude(m => m.Size)
                    .Include(m => m.Brand)
                    .Skip((page * take) - take)
                    .Take(take)
                    .ToListAsync();
            }

            if (categoryId != null)
            {
                products = await _context.Products
                    .Include(m => m.ProductImages)
                    .Include(m => m.Category)
                    .Where(m => m.Category.Id == categoryId)
                    .Skip((page * take) - take)
                    .Take(take)
                    .ToListAsync();
            }


            if (subCategoryId != null)
            {

                //products =  await _context.CategorySubCategories
                //     .Include(m=>m.SubCategory)
                //     .ThenInclude(m=>m.)


                products = await _context.Products
                    .Include(m => m.ProductImages)
                    .Include(m => m.SubCategory)
                    .Where(m => m.SubCategory.Id == subCategoryId)
                    .Skip((page * take) - take)
                    .Take(take)
                    .ToListAsync();

                //return await _context.Products
                //.Include(m => m.CategorySubCategories)
                //.ThenInclude(m => m.Category)
                //.Include(m => m.CategorySubCategories)
                //.ThenInclude(m => m.SubCategory)
                //.Select(m => m.CategoryId == categoryId)
                //.Select(m => m)
                //.Skip((page * take) - take)
                //.Take(take)
                //.ToListAsync();
            }


            if(colorId != null)
            {
                products = await _context.ProductColors
                    .Include(m => m.Product)
                    .ThenInclude(m => m.ProductImages)
                    .Where(m => m.ColorId == colorId)
                    .Skip((page * take) - take)
                    .Take(take)
                    .Select(m => m.Product)
                    .ToListAsync();
            }

            if(sizeId != null)
            {
                products = await _context.ProductSizes
                    .Include(m => m.Product)
                    .ThenInclude(m=>m.ProductImages)
                    .Where(m=>m.SizeId== sizeId)
                    .Skip((page*take) - take)
                    .Take(take)
                    .Select(m=>m.Product)
                    .ToListAsync();
            }

            if(brandId != null)
            {
                products = await _context.Products
                    .Include(m => m.ProductImages)
                    .Include(m => m.Brand)
                    .Where(m => m.BrandId == brandId)
                    .Skip((page * take) - take)
                    .Take(take)
                    .ToListAsync();
            }


            return products;

        }


        public async Task<List<ProductVM>> GetProductsBySubCategoryIdAsync(int? id, int page = 1, int take = 6)
        {
            List<ProductVM> model = new();
            var products = await _context.Products
                 .Include(m => m.ProductImages)
                 .Include(m => m.SubCategory)
                 .Where(m => m.SubCategoryId == id)
                 .Skip((page * take) - take)
                 .Take(take)
                 .ToListAsync();

            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    DiscountPrice = item.DiscountPrice,
                    Name = item.Name,
                    ProductImages = item.ProductImages,
                    Rating = item.Rating
                });
            }
            return model;

        }


        public async Task<List<ProductVM>> GetProductsByColorIdAsync(int? id)
        {
            List<ProductVM> model = new();
            var products = await _context.ProductColors
                .Include(m => m.Product)
                .ThenInclude(m => m.ProductImages)
                .Where(m => m.Color.Id == id)
                .Select(m => m.Product)
                .ToListAsync();

            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    DiscountPrice = item.DiscountPrice,
                    Name = item.Name,
                    ProductImages = item.ProductImages,
                    Rating = item.Rating
                });
            }
            return model;
        }

        public async Task<List<ProductVM>> GetProductsBySizeIdAsync(int? id)
        {
            List<ProductVM> model = new();
            var products = await _context.ProductSizes
                .Include(m => m.Product)
                .ThenInclude(m => m.ProductImages)
                .Where(m => m.Size.Id == id)
                .Select(m => m.Product)
                .ToListAsync();

            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    DiscountPrice = item.DiscountPrice,
                    Name = item.Name,
                    ProductImages = item.ProductImages,
                    Rating = item.Rating
                });
            }
            return model;
        }


        public async Task<List<ProductVM>> GetProductsByBrandIdAsync(int? id, int page = 1, int take = 6)
        {
            List<ProductVM> model = new();
            var products = await _context.Products
                .Include(m => m.ProductImages)
                .Include(m => m.Brand)
                .Where(p => p.Brand.Id == id)
                 .Skip((page * take) - take)
                 .Take(take)
                 .ToListAsync();

            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    DiscountPrice= item.DiscountPrice,
                    Name = item.Name,
                    ProductImages = item.ProductImages,
                    Rating = item.Rating
                });
            }
            return model;
        }

        public async Task<IEnumerable<ProductVM>> GetDatasAsync()
        {
            List<ProductVM> model = new();
            var products = await _context.Products.Include(m => m.ProductImages).ToListAsync();
            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    DiscountPrice= item.DiscountPrice,
                    Name = item.Name,
                    ProductImages = item.ProductImages,
                    Rating = item.Rating
                });
            }
            return model;
        }

        public async Task<int> GetProductsCountByColorAsync(int? colorId)
        {
            return await _context.ProductColors
                  .Include(m => m.Product)
                  .ThenInclude(m => m.ProductImages)
                  .Where(m => m.Color.Id == (int)colorId)
                  .Select(m => m.Product)
                  .CountAsync();
        }



        public async Task<int> GetProductsCountBySizeAsync(int? sizeId)
        {
            return await _context.ProductSizes
          .Include(m => m.Product)
          .ThenInclude(m => m.ProductImages)
          .Where(m => m.Size.Id == (int)sizeId)
          .Select(m => m.Product)
          .CountAsync();
        }


        public async Task<int> GetProductsCountByBrandAsync(int? brandId)
        {
            return await _context.Products
                   .Include(p => p.ProductImages)
                   .Include(c => c.Brand)
                   .Where(p => p.Brand.Id == (int)brandId)
                   .CountAsync();
        }


        public async Task<List<Product>> GetAllBySearchText(string searchText)
        {
            var products = await _context.Products
            .Include(m => m.ProductImages)
            .OrderByDescending(m => m.Id)
            .Where(m => m.Name.ToLower().Contains(searchText.ToLower()))
            .ToListAsync();
            return products;
        }

        public async Task<List<ProductComment>> GetComments()
        {
            return await _context.ProductComments.Include(m => m.Product).ToListAsync();
        }


        public async Task<ProductComment> GetCommentByIdWithProduct(int? id)
        {
            return await _context.ProductComments.Include(m => m.Product).FirstOrDefaultAsync(m => m.Id == id);
        }



        public async Task<ProductComment> GetCommentById(int? id)
        {
            return await _context.ProductComments.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> GetProductsCountBySubCategoryAsync(int? id)
        {
            return await _context.Products
                 .Include(p => p.SubCategory)
                 .Include(p => p.ProductImages)
                 .Where(pc => pc.SubCategory.Id == id)
                 .CountAsync();
        }

        public async Task<IQueryable<Product>> FilterByName(string name)
        {
            return _context.Products
                .Include(pr => pr.ProductColors)
                .Include(pr => pr.ProductSizes)
                .Include(pr => pr.Brand)
                .Where(pr => !string.IsNullOrEmpty(name) ? pr.Name.ToLower().Contains(name.ToLower()) : true);
        }



        public async Task<List<ProductVM>> GetProductsByCategoryIdAsync(int? id, int page = 1, int take = 6)
        {
            List<ProductVM> model = new();
            List<Product> products = await _context.Products
                .Include(m => m.ProductImages)
                .Include(m => m.Category)
                .Where(m => m.Category.Id == id)
                .Skip((page * take) - take)
                .Take(take)
                .ToListAsync();

            foreach (var item in products)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Price = item.Price,
                    DiscountPrice = item.DiscountPrice,
                    Name = item.Name,
                    ProductImages = item.ProductImages,
                    Rating = item.Rating
                });
            }
            return model;
        }

        public async Task<int> GetProductsCountByCategoryAsync(int? id)
        {
                    return await _context.Products
                          .Include(p => p.Category)
                          .Include(p => p.ProductImages)
                          .Where(pc => pc.Category.Id == id)
                          .CountAsync();
        }

        public async Task<Product> GetDatasModalProductByIdAsyc(int? id)
        {
            return await _context.Products.Include(m => m.ProductImages)
            .Include(m => m.Category)
            .Include(m => m.Brand)
            .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
