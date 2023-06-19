using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.Services.Interfaces
{
    public interface ISubCategoryService
    {
        Task<List<SubCategory>> GetAllAsync();
        Task<SubCategory> GetByIdAsync(int? id);
        bool CheckByName(string name);

    }
}
