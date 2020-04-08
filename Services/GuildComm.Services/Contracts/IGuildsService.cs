namespace GuildComm.Services
{
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels;

    using System.Threading.Tasks;
    using System.Collections.Generic;
    using GuildComm.Web.ViewModels.Guild;

    public interface IGuildsService
    {
        Task AddMemberAsync(int characterId, string rank, string guildId);

        Task CreateGuildAsync(GuildCreateInputModel guild);

        Task<Guild> GetGuildByIdAsync(string id);

        Task<GuildDetailsViewModel> GetGuildViewModelByIdAsync(string id);

        Task<GuildManageViewModel> GetGuildManageViewModelByIdAsync(string id);

        Task<List<GuildsAllViewModel>> GetAllGuildsAsync();

        Task<List<GuildsAllViewModel>> GetUserGuildsAsync();

        Task<bool> IsUserInTargetGuild(string guildId);

        Task<bool> IsUserAuthorized(string guildId);

        Task PromoteMemberAsync(string id);

        Task DemoteMemberAsync(string id);

        Task RemoveGuildAsync(string id);

        Task RemoveMemberAsync(string id);
    }
}
