using AutoMapper;
using GuildComm.Common.Constants;
using GuildComm.Core.Interfaces;
using GuildComm.Data.Models.Identity;
using GuildComm.Web.Models.Account;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GuildComm.Core
{
    public class IdentityService : IIdentityService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<GuildCommUser> _userManager;
        private readonly SignInManager<GuildCommUser> _signInManager;
        private readonly IEmailService _emailService;

        public IdentityService(IMapper mapper, UserManager<GuildCommUser> userManager, SignInManager<GuildCommUser> signInManager, IEmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<IdentityResult> CreateUserAsync(UserRegistrationInputModel inputModel, ControllerBase controller)
        {
            var user = _mapper.Map<GuildCommUser>(inputModel);
            var result = await _userManager.CreateAsync(user, inputModel.Password);

            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationUrl = controller.Url.Action(MvcConstants.Action.ConfirmEmail, MvcConstants.Controller.Account, new { token, email = user.Email }, controller.Request.Scheme);

                this.SendConfirmationEmail(user.Email, confirmationUrl);
            }

            return result;
        }

        public async Task<bool> ConfirmUserEmailAsync(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            var confirmation = await _userManager.ConfirmEmailAsync(user, token);
            return confirmation.Succeeded;
        }

        public async Task<bool> SignInUserAsync(HttpContext context, UserLoginInputModel inputModel)
        {
            var result = await _signInManager.PasswordSignInAsync(inputModel.Email, inputModel.Password, inputModel.RememberMe, false);
            return result.Succeeded;
        }

        public async Task SignOutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task RequestUserResetPassword(UserForgotPasswordInputModel inputModel, ControllerBase controller)
        {
            var user = await _userManager.FindByEmailAsync(inputModel.Email);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callback = controller.Url.Action(MvcConstants.Action.ResetPassword, MvcConstants.Controller.ForgotPassword, new { token, email = user.Email }, controller.Request.Scheme);

                _emailService.SendEmail(new string[] { user.Email }, EmailSubjects.ResetPassword, callback);
            }        
        }

        public async Task<GuildCommUser> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<string> GetPasswordResetTokenAsync(GuildCommUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        public async Task<IdentityResult> ResetUserPasswordAsync(GuildCommUser user, string token, string password)
        {
            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, password);
            return resetPasswordResult;
        }

        private void SendConfirmationEmail(string email, string confirmationLink)
        {
            _emailService.SendEmail(new string[] { email }, EmailSubjects.EmailConfirmation, confirmationLink);
        }
    }
}
