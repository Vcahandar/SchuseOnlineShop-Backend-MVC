using SchuseOnlineShop.Models;

namespace SchuseOnlineShop.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<Team>> GetTeamsAll();

        Task<Team> GetByIdAsync(int? id);



    }
}
