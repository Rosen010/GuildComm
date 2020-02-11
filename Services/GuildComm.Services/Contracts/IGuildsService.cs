namespace GuildComm.Services
{
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels;

    using System.Threading.Tasks;
    using System.Collections.Generic;
    using GuildComm.Web.ViewModels.Guild;

    public interface IGuildsService
    {
        Task CreateGuildAsync(GuildCreateInputModel guild);

        Task<Guild> GetGuildAsync(string name);

        Task<GuildDetailsViewModel> GetGuildByIdAsync(string id);

        Task<List<GuildsAllViewModel>> GetAllGuildsAsync();

        Task<List<GuildsAllViewModel>> GetUserGuildsAsync();
    }
}
