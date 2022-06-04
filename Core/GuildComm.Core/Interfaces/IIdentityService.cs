using GuildComm.Data.Models.Identity;
using GuildComm.Web.Models.Account;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuildComm.Core.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityResult> CreateUserAsync(UserRegistrationInputModel inputModel, ControllerBase controller);

        Task<bool> ConfirmUserEmailAsync(string token, string email);

        Task<bool> SignInUserAsync(HttpContext context, UserLoginInputModel inputModel);

        Task SignOutUserAsync();

        Task<GuildCommUser> GetUserByEmailAsync(string email);

        Task<string> GetPasswordResetTokenAsync(GuildCommUser user);

        Task<IdentityResult> ResetUserPasswordAsync(GuildCommUser user, string token, string password);

        Task RequestUserResetPassword(UserForgotPasswordInputModel inputModel, ControllerBase controller);
    }
}
