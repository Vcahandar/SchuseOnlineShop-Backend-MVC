using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.Services.Interfaces
{
    public interface IColorService
    {
        Task<IEnumerable<Color>> GetAllAsync();
        Task<Color> GetByIdAsync(int? id);
        bool CheckByName(string name);

        Task<int> GetCountAsync();

        Task<List<Color>> GetPaginatedDatasAsync(int page, int take);
    }
}
