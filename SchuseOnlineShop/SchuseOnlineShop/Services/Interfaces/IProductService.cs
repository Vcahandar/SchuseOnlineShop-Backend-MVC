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
        Task<Product> GetDatasModalProductByIdAsyc(int? id);

        Task<IEnumerable<ProductVM>> GetDatasAsync();
        Task<ProductImage> GetImageById(int? id);
        Task<Product> GetProductByImageId(int? id);
        void RemoveImage(ProductImage image);

        Task<int> GetCountAsync();
        Task<List<Product>> GetPaginatedDatasAsync(int page, int take, int? categoryId, int? subCategoryId, int? colorId, int? brandId,int? value1, int? value2);

        Task<int> GetProductsCountByRangeAsync(int? value1, int? value2);
        Task<List<ProductVM>> GetProductsBySubCategoryIdAsync(int? id, int page, int take);
        Task<List<ProductVM>> GetProductsByColorIdAsync(int? id, int page, int take);
        Task<List<ProductVM>> GetProductsByBrandIdAsync(int? id, int page, int take);

        Task<int> GetProductsCountByColorAsync(int? colorId);

        Task<int> GetProductsCountBySubCategoryAsync(int? subCatId);
        Task<int> GetProductsCountByCategoryAsync(int? catId);
        Task<int> GetProductsCountByBrandAsync(int? brandId);


        Task<List<Product>> GetAllBySearchText(string searchText);
        Task<List<ProductComment>> GetComments();
        Task<ProductComment> GetCommentByIdWithProduct(int? id);
        Task<ProductComment> GetCommentById(int? id);

        Task<IQueryable<Product>> FilterByName(string? name);

        Task<List<ProductVM>> GetProductsByCategoryIdAsync(int? id, int page, int take);

        Task<int> GetProductsCountBySortTextAsync(string sortValue);






    }
}
