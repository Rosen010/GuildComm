using GuildComm.Services.Contracts;
using GuildComm.Services.Contracts.Clients;

namespace GuildComm.Services
{
    public class GuildService : IGuildService
    {
        private readonly IBNetGuildClient _guildClient;

        public GuildService(IBNetGuildClient guildClient)
        {
            _guildClient = guildClient;
        }
    }
}
