namespace GuildComm.Services
{
    using GuildComm.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGuildsService
    {
        Task CreateGuildAsync(Guild guild);

        Task<Guild> GetGuildAsync(string name);

        Task<List<Guild>> GetAllGuildsAsync();
    }
}
