using GuildComm.Common;
using GuildComm.Common.Constants;
using GuildComm.Core.Interfaces;
using GuildComm.Web.Models.Account;

using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace GuildComm.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;

        public AccountController(IIdentityService identityService, IEmailService emailService)
        {
            _identityService = identityService;
            _emailService = emailService;
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

            var result = await _identityService.CreateUserAsync(userModel, this);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return this.View(userModel);
            }

            return this.RedirectToAction(nameof(SuccessfulRegistration));
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
                ModelState.AddModelError(string.Empty, ErrorMessages.InvalidLogin);
                return this.View();
            }

            return this.RedirectToLocal(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _identityService.SignOutUserAsync();
            return RedirectToAction(nameof(HomeController.Index), MvcConstants.Controller.Home);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var confirmedEmail = await _identityService.ConfirmUserEmailAsync(token, email);

            if (!confirmedEmail)
            {
                return this.Redirect(string.Format(ViewNames.ErrorPage, HttpStatusCode.InternalServerError));
            }

            return this.View();
        }

        [HttpGet]
        public IActionResult SuccessfulRegistration()
        {
            return this.View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction(nameof(HomeController.Index), MvcConstants.Controller.Home);
        }
    }
}
