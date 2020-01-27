﻿namespace GuildComm.Services
{
    using GuildComm.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRealmsService
    {
        IList<Realm> GetAllRealms();

        Realm GetRealm(string name);
    }
}
