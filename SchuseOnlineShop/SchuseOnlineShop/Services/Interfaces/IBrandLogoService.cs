using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.Services.Interfaces
{
    public interface IBrandLogoService
    {
        Task<IEnumerable<BrandLogo>> GetBrandLogosAll();
    }
}
