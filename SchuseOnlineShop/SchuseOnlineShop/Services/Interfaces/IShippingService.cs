using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.Services.Interfaces
{
    public interface IShippingService
    {
        Task<IEnumerable<Shipping>> GetAll();

    }
}
