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

        public UserController(UserManager<GuildCommUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Info()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail))
            {
                return this.RedirectToAction(nameof(HomeController.Index), MvcConstants.Controller.Home);
            }

            var user = await _userManager.FindByEmailAsync(userEmail);

            return this.View();
        }
    }
}
