using AutoMapper;
using BNetAPI.Core.Components.Guilds;
using BNetAPI.Core.Components.Guilds.Models.RequestModels;
using GuildComm.Common.Constants;
using GuildComm.Core.Interfaces;
using GuildComm.Web.Models.Guild;
using GuildComm.Web.Models.Items;

using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GuildComm.Services
{
    public class GuildService : IGuildService
    {
        private readonly IMapper _mapper;
        private readonly IBNetGuildClient _guildClient;

        private const int DefaultPageSize = 20;

        public GuildService(IMapper mapper, IBNetGuildClient guildClient)
        {
            _mapper = mapper;
            _guildClient = guildClient;
        }

        public async Task<GuildViewModel> FindGuiildAsync(GuildInputModel model)
        {
            var guildRequest = _mapper.Map<GuildRequestModel>(model);
            var rosterRequest = _mapper.Map<RosterRequestModel>(model);

            var guildResponse = await _guildClient.RequestGuildAsync(guildRequest);
            var rosterResponse = await _guildClient.RequestRosterAsync(rosterRequest);

            if (guildResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                var viewModel = _mapper.Map<GuildViewModel>(guildResponse);

                viewModel.Locale = guildRequest.Locale;
                viewModel.NameSpace = guildRequest.NameSpace;
                viewModel.CurrentPage = model.CurrentPage;

                viewModel.DisablePrevButton = model.CurrentPage == 0 ? HtmlConstants.Disabled : string.Empty;
                viewModel.DisableNextButton = (model.CurrentPage + 1) * DefaultPageSize >= rosterResponse.Members.Count ? HtmlConstants.Disabled : string.Empty;

                viewModel.Members = rosterResponse.Members
                    .OrderBy(m => m.Rank)
                    .Skip(model.CurrentPage * DefaultPageSize)
                    .Take(DefaultPageSize)
                    .Select(m => _mapper.Map<MemberItem>(m))
                    .ToList();

                return viewModel;
            }

            return null;
        }
    }
}
