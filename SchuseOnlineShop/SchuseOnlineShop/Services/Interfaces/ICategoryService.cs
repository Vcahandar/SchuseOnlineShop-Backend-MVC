using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int? id);

    }
}
