using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int? id);

        Task<List<Blog>> GetPaginatedDatasAsync(int page, int take);

        Task<int> GetCountAsync();


    }
}
