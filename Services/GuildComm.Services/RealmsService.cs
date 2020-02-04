namespace GuildComm.Services
{
    using GuildComm.Data;
    using GuildComm.Data.Models;

    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class RealmsService : IRealmsService
    {
        private readonly GuildCommDbContext context;

        public RealmsService(GuildCommDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Realm>> GetAllRealmsAsync()
        {
            List<Realm> realms = await this.context.Realms.ToListAsync();
            return realms;
        }

        public async Task<Realm> GetRealmByNameAsync(string name)
        {
            Realm realm = await this.context.Realms.SingleOrDefaultAsync(dbRealm => dbRealm.Name == name);

            return realm;
        }

        public async Task<Realm> GetRealmByIdAsync(int id)
        {
            Realm realm = await this.context.Realms.SingleOrDefaultAsync(dbRealm => dbRealm.Id == id);

            return realm;
        }
    }
}
