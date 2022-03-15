using GuildComm.Data.Enums;
using GuildComm.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GuildComm.Data.Repositories
{
    public class RealmsRepository : IRealmsRepository
    {
        private readonly GuildCommDbContext _dbContext;

        public RealmsRepository(GuildCommDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> GetRealmNamesByRegionAsync(Region region)
        {
            var realms = await _dbContext.Realms
                .Where(r => r.Region.Equals(region))
                .Select(r => r.Name)
                .ToListAsync();

            return realms;
        }
    }
}
