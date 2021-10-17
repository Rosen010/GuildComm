using GuildComm.Data.Models;
using GuildComm.Data.Repositories.Contracts;
using System.Collections.Generic;

namespace GuildComm.Data.Repositories
{
    public class RealmRepository : IRealmRepository
    {
        private readonly GuildCommDbContext _context;

        public RealmRepository(GuildCommDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Realm> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void InsertAll(IEnumerable<Realm> realms)
        {
            throw new System.NotImplementedException();
        }
    }
}
