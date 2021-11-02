using GuildComm.Services.Models.RequestModels;
using GuildComm.Services.Models.ResponseModels;
using System.Threading.Tasks;

namespace GuildComm.Services.Contracts.Clients
{
    public interface IBNetGuildClient
    {
        Task<GuildResponse> RetrieveGuild(GuildRequestModel request);
    }
}
