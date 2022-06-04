using GuildComm.Core.Interfaces;
using GuildComm.Web.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuildComm.Web.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;

        public ForgotPasswordController(IIdentityService identityService, IEmailService emailService)
        {
            _identityService = identityService;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(UserForgotPasswordInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            await _identityService.RequestUserResetPassword(inputModel, this);

            return this.RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new UserResetPasswordInputModel
            {
                Token = token,
                Email = email,
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(UserResetPasswordInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await _identityService.GetUserByEmailAsync(inputModel.Email);
            
            if (user == null)
            {
                return this.RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            var resetPasswordResult = await _identityService.ResetUserPasswordAsync(user, inputModel.Token, inputModel.Password);

            if (!resetPasswordResult.Succeeded)
            {
                foreach (var error in resetPasswordResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return this.View(inputModel);
            }

            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return this.View();
        }
    }
}
