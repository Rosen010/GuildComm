using GuildComm.Data.Enums;

namespace GuildComm.Data.Repositories.Interfaces
{
    public interface IRealmsRepository
    {
        Task<IEnumerable<string>> GetRealmNamesByRegionAsync(Region region);
    }
}
