namespace GuildComm.Services
{
    using GuildComm.Data;
    using GuildComm.Data.Models;

    using System.Linq;
    using System.Collections.Generic;

    public class RealmsService : IRealmsService
    {
        private readonly GuildCommDbContext context;

        public RealmsService(GuildCommDbContext context)
        {
            this.context = context;
        }

        public IList<Realm> GetAllRealms()
        {
            List<Realm> realms = this.context.Realms.ToList();
            return realms;
        }

        public Realm GetRealm(string name)
        {
            Realm realm = this.context.Realms.FirstOrDefault(dbRealm => dbRealm.Name == name);

            return realm;
        }
    }
}
