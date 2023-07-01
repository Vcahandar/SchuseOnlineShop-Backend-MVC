using SchuseOnlineShop.Models;
using SchuseOnlineShop.ViewModels.Product;

namespace SchuseOnlineShop.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int? id);
        Task<List<Product>> GetFullDataAsync();
        Task<Product> GetFullDataByIdAsync(int? id);

        Task<IEnumerable<ProductVM>> GetDatasAsync();
        Task<ProductImage> GetImageById(int? id);
        Task<Product> GetProductByImageId(int? id);
        void RemoveImage(ProductImage image);
        Task<ProductVideo> GetVideoById(int? id);
        Task<Product> GetProductByVideoId(int? id);
        void RemoveVideo(ProductVideo video);
        Task<int> GetCountAsync();
        Task<List<Product>> GetPaginatedDatasAsync(int page, int take, int? categoryId, int? colorId, int? brandId, int? sizeId);

        Task<List<ProductVM>> GetProductsBySubCategoryIdAsync(int? id, int page, int take);
        Task<List<ProductVM>> GetProductsByColorIdAsync(int? id);
        Task<List<ProductVM>> GetProductsBySizeIdAsync(int? id);
        Task<List<ProductVM>> GetProductsByBrandIdAsync(int? id);

        Task<int> GetProductsCountByColorAsync(int? colorId);
        Task<int> GetProductsCountBySizeAsync(int? sizeId);

        Task<int> GetProductsCountBySubCategoryAsync(int? catId);
        Task<int> GetProductsCountByBrandAsync(int? brandId);

        Task<List<Product>> GetAllBySearchText(string searchText);
        Task<List<ProductComment>> GetComments();
        Task<ProductComment> GetCommentByIdWithProduct(int? id);
        Task<ProductComment> GetCommentById(int? id);

        Task<IQueryable<Product>> FilterByName(string? name);

        Task<List<ProductVM>> GetProductsByCategoryIdAsync(int? id, int page, int take);





    }
}
