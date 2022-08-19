using BNetAPI.Accounts.Interfaces;
using GuildComm.Common.Constants;
using GuildComm.Data.Models.Identity;
using GuildComm.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GuildComm.Web.Areas.Controllers
{
    [Authorize]
    [Area("profile")]
    public class UserController : Controller
    {
        private readonly UserManager<GuildCommUser> _userManager;
        private readonly IBNetAccountClient _accountClient;

        public UserController(UserManager<GuildCommUser> userManager, IBNetAccountClient accountClient)
        {
            _userManager = userManager;
            _accountClient = accountClient;
        }

        public async Task<IActionResult> Info([FromQuery(Name = "code")] string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var token = await _accountClient.GetUserAccessTokenAsync(code);
                var accountInfo = await _accountClient.GetUserAccountInfo(token);
            }

            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail))
            {
                return this.RedirectToAction(nameof(HomeController.Index), MvcConstants.Controller.Home);
            }

            var user = await _userManager.FindByEmailAsync(userEmail);

            return this.View();
        }

        public IActionResult SyncAccount()
        {
            var authenticationUrl = _accountClient.GetAuthenticationUrl();
            return this.Redirect(authenticationUrl);
        }
    }
}
