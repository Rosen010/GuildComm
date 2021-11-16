using GuildComm.Data.Models;
using System.Threading.Tasks;

namespace GuildComm.Data.Repositories.Contracts
{
    public interface ITokenRepository
    {
        Task<AccessToken> GetTokenAsync(string name);

        Task UpdateTokenAsync(AccessToken accessToken);
    }
}
