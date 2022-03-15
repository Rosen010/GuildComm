using GuildComm.Core.Interfaces;
using GuildComm.Data.Enums;
using GuildComm.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuildComm.Core
{
    public class RealmService : IRealmService
    {
        private readonly IRealmsRepository _realmsRepository;

        public RealmService(IRealmsRepository realmsRepository)
        {
            _realmsRepository = realmsRepository;
        }

        public async Task<IEnumerable<string>> GetRealmsByRegionAsync(string region)
        {
            Enum.TryParse(region, out Region parsedRegion);

            var realms = await _realmsRepository.GetRealmNamesByRegionAsync(parsedRegion);
            return realms;
        }
    }
}
