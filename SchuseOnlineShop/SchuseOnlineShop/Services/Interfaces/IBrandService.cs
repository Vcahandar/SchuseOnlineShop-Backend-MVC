using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.Services.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<Brand> GetByIdAsync(int? id);
        bool CheckByName(string name);

        Task<int> GetCountAsync();

        Task<List<Brand>> GetPaginatedDatasAsync(int page, int take);

    }
}
