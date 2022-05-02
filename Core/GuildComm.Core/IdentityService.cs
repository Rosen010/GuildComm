using AutoMapper;

using GuildComm.Core.Interfaces;
using GuildComm.Data.Models.Identity;
using GuildComm.Web.Models.Account;

using Microsoft.AspNetCore.Identity;

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
    }
}
