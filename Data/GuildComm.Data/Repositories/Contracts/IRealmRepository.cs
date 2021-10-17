using GuildComm.Data.Models;
using System.Collections.Generic;

namespace GuildComm.Data.Repositories.Contracts
{
    public interface IRealmRepository
    {
        IEnumerable<Realm> GetAll();

        void InsertAll(IEnumerable<Realm> realms);
    }
}
