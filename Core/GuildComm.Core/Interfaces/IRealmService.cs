using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuildComm.Core.Interfaces
{
    public interface IRealmService
    {
        Task<IEnumerable<string>> GetRealmsByRegionAsync(string region);
    }
}
