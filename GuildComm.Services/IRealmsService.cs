namespace GuildComm.Services
{
    using GuildComm.Data.Models;
    using System.Collections.Generic;

    public interface IRealmsService
    {
        IList<Realm> GetAllRealms();

        Realm GetRealm(string name);
    }
}
