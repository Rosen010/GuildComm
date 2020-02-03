namespace GuildComm.Services
{
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels;

    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IGuildsService
    {
        Task CreateGuildAsync(GuildCreateInputModel guild);

        Task<Guild> GetGuildAsync(string name);

        Task<List<GuildsAllViewModel>> GetAllGuildsAsync();

        Task<List<GuildsAllViewModel>> GetUserGuildsAsync();
    }
}
