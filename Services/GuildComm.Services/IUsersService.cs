namespace GuildComm.Services
{
    using System.Threading.Tasks;
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels.Users;

    public interface IUsersService
    {
        Task<GuildCommUser> GetUserAsync();

        Task<GuildCommUserDetailsViewModel> GetUserViewModelAsync();       
    }
}
