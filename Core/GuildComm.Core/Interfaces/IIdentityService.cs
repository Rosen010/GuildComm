using GuildComm.Web.Models.Account;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using System.Threading.Tasks;

namespace GuildComm.Core.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityResult> CreateUserAsync(UserRegistrationInputModel inputModel);

        Task<bool> SignInUserAsync(HttpContext context, UserLoginInputModel inputModel);
    }
}
