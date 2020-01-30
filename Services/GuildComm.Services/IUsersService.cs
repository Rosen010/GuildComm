namespace GuildComm.Services
{
    using System.Threading.Tasks;

    using GuildComm.Web.ViewModels.Users;

    public interface IUsersService
    {
        Task<GuildCommUserDetailsViewModel> GetUserAsync();
    }
}
