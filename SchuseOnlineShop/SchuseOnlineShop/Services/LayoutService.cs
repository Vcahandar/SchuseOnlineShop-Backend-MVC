using Microsoft.EntityFrameworkCore;
using SchuseOnlineShop.Data;
using SchuseOnlineShop.Models;
using SchuseOnlineShop.Services.Interfaces;

namespace SchuseOnlineShop.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;
        public LayoutService(AppDbContext context)
        {
            _context = context;
        }


        public Setting GetById(int? id)
        {
            return _context.Settings.Where(m => m.Id == id).FirstOrDefault();
        }

        public async Task<SectionHeader> GetSectionAsync(int? id)
        {
            return await _context.SectionHeaders.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<SectionHeader>> GetSectionsDatasAsync()
        {
            return await _context.SectionHeaders.ToListAsync();
        }

        public Dictionary<string, string> GetSectionsHeaders()
        {
            return _context.SectionHeaders.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
        }

        public async Task<List<Setting>> GetSettingDatas()
        {
            List<Setting> settings = await _context.Settings.ToListAsync();

            return settings;
        }

        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
        }
    }
}
