namespace GuildComm.Services
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels.Users;

    public interface IUsersService
    {
        Task<GuildCommUser> GetUserAsync();

        Task<GuildCommUserDetailsViewModel> GetUserViewModelAsync();

        Task<List<GuildCommUserViewModel>> GetAllUsersAsync(int? take = null, int skip = 0);

        int GetUsersCount();

        Task RemoveUserAsync(string id);

        Task UpdateUserDescriptionAsync(GuildCommUserDescriptionUpdateInputModel inputModel);
    }
}
