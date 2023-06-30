using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.Services.Interfaces
{
    public interface IAdvertService
    {
        Task<IEnumerable<Advert>> GetAllAsync();
        Task<Advert> GetByIdAsync(int? id);
    }
}
