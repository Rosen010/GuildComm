using GuildComm.Data.Enums;
using GuildComm.Data.Models;

namespace GuildComm.Data.Repositories.Interfaces
{
    public interface IRealmsRepository
    {
        Task<IEnumerable<Realm>> GetRealmsByRegion(Region region);
    }
}
