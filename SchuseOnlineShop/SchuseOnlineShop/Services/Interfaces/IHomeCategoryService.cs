using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.Services.Interfaces
{
    public interface IHomeCategoryService
    {
        Task<IEnumerable<HomeCategory>> GetAllAsync();
        Task<HomeCategory> GetByIdAsync(int id);

    }
}
