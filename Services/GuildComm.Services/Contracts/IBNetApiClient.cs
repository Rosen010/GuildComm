using GuildComm.Services.Models;
using System.Threading.Tasks;

namespace GuildComm.Services.Contracts
{
    public interface IBNetApiClient
    {
        Task<string> GetAccessToken();
    }
}
