using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.Services.Interfaces
{
    public interface IBrandLogoService
    {
        Task<IEnumerable<BrandLogo>> GetBrandLogosAll();
        Task<BrandLogo> GetByIdAsync(int? id);

    }
}
