using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Services
{
    public class ShippingService : IShippingService
    {
        public Task<IEnumerable<Shipping>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
