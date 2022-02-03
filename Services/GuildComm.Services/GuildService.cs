using AutoMapper;
using BNetAPI.Core.Models;
using BNetAPI.Guilds.Interfaces;
using BNetAPI.Guilds.Models.RequestModels;
using GuildComm.Core.Interfaces;
using GuildComm.Web.Models.Guild;
using GuildComm.Web.Models.Search;
using System.Threading.Tasks;

namespace GuildComm.Services
{
    public class GuildService : IGuildService
    {
        private readonly IMapper _mapper;
        private readonly IBNetGuildClient _guildClient;

        public GuildService(IMapper mapper, IBNetGuildClient guildClient)
        {
            _mapper = mapper;
            _guildClient = guildClient;
        }

        public async Task<GuildViewModel> FindGuiild(SearchInputModel model)
        {
            var requestModel = _mapper.Map<GuildRequestModel>(model);
            requestModel.Locale = "en_GB";
            var responseModel = await _guildClient.RetrieveGuild(requestModel);

            var viewModel = _mapper.Map<GuildViewModel>(responseModel);
            return viewModel;
        }
    }
}
