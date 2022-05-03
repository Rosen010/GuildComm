using AutoMapper;

using GuildComm.Core.Interfaces;
using GuildComm.Data.Models.Identity;
using GuildComm.Web.Models.Account;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

using System.Security.Claims;
using System.Threading.Tasks;

namespace GuildComm.Core
{
    public class IdentityService : IIdentityService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<GuildCommUser> _userManager;

        public IdentityService(IMapper mapper, UserManager<GuildCommUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(UserRegistrationInputModel inputModel)
        {
            var user = _mapper.Map<GuildCommUser>(inputModel);
            var result = await _userManager.CreateAsync(user, inputModel.Password);

            return result;
        }

        public async Task<bool> SignInUserAsync(HttpContext context, UserLoginInputModel inputModel)
        {
            var user = await _userManager.FindByEmailAsync(inputModel.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, inputModel.Password))
            {
                var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                await context.SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(identity));

                return true;
            }

            return false;
        }
    }
}
