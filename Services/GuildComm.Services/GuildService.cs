using AutoMapper;
using BNetAPI.Core.Models;
using BNetAPI.Guilds.Interfaces;
using BNetAPI.Guilds.Models.RequestModels;
using GuildComm.Core.Interfaces;
using GuildComm.Web.Models.Guild;
using GuildComm.Web.Models.Items;
using GuildComm.Web.Models.Search;
using System.Linq;
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

        public async Task<GuildViewModel> FindGuiildAsync(SearchInputModel model)
        {
            var guildRequest = _mapper.Map<GuildRequestModel>(model);
            var rosterRequest = _mapper.Map<RosterRequestModel>(model);

            var guildResponse = await _guildClient.RequestGuildAsync(guildRequest);
            var rosterResponse = await _guildClient.RequestRosterAsync(rosterRequest);

            var viewModel = _mapper.Map<GuildViewModel>(guildResponse);
            viewModel.Locale = guildRequest.Locale;
            viewModel.NameSpace = guildRequest.NameSpace;

            viewModel.Members = rosterResponse.Members
                .OrderBy(m => m.Rank)
                .Take(20)
                .Select(m => _mapper.Map<MemberItem>(m))
                .ToList();

            return viewModel;
        }
    }
}
