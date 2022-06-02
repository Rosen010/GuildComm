using GuildComm.Core.Interfaces;
using GuildComm.Web.Models.Account;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace GuildComm.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationInputModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(userModel);
            }

            var result = await _identityService.CreateUserAsync(userModel);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return this.View(userModel);
            }

            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginInputModel userModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return this.View(userModel);
            }

            var successfulLogin = await _identityService.SignInUserAsync(this.HttpContext, userModel);

            if (!successfulLogin)
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return this.View();
            }

            return this.RedirectToLocal(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _identityService.SignOutUserAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
