﻿namespace GuildComm.Services
{
    using GuildComm.Data;
    using GuildComm.Data.Models;

    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class RealmsService : IRealmsService
    {
        private readonly GuildCommDbContext context;

        public RealmsService(GuildCommDbContext context)
        {
            this.context = context;
        }

        public async Task<IList<Realm>> GetAllRealmsAsync()
        {
            List<Realm> realms = await this.context.Realms.ToListAsync();
            return realms;
        }

        public async Task<Realm> GetRealmAsync(string name)
        {
            Realm realm = await this.context.Realms.SingleOrDefaultAsync(dbRealm => dbRealm.Name == name);

            return realm;
        }
    }
}