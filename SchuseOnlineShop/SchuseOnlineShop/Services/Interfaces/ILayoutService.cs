using SchuseOnlineShop.Models;
using SchuseOnlineShop.ViewModels.Cart;

namespace SchuseOnlineShop.Services.Interfaces
{
    public interface ILayoutService
    {
        Task<List<Setting>> GetSettingDatas();
        Dictionary<string, string> GetSettings();
        Dictionary<string, string> GetSectionsHeaders();
        Setting GetById(int? id);
        Task<IEnumerable<SectionHeader>> GetSectionsDatasAsync();
        Task<SectionHeader> GetSectionAsync(int? id);

    }
}
