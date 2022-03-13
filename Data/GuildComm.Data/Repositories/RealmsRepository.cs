using GuildComm.Data.Enums;
using GuildComm.Data.Models;
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

        public async Task<IEnumerable<Realm>> GetRealmsByRegion(Region region)
        {
            var realms = await _dbContext.Realms.Where(r => r.Region.Equals(region)).ToListAsync();
            return realms;
        }
    }
}
