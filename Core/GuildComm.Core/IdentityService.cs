using AutoMapper;

using GuildComm.Core.Interfaces;
using GuildComm.Data.Models.Identity;
using GuildComm.Web.Models.Account;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using System.Threading.Tasks;

namespace GuildComm.Core
{
    public class IdentityService : IIdentityService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<GuildCommUser> _userManager;
        private readonly SignInManager<GuildCommUser> _signInManager;

        public IdentityService(IMapper mapper, UserManager<GuildCommUser> userManager, SignInManager<GuildCommUser> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(UserRegistrationInputModel inputModel)
        {
            var user = _mapper.Map<GuildCommUser>(inputModel);
            var result = await _userManager.CreateAsync(user, inputModel.Password);

            return result;
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
    }
}
