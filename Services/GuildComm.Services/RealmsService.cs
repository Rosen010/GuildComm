namespace GuildComm.Services
{
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels.Realms;

    using AutoMapper;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System;
    using GuildComm.Data.Models.Enums;

    public class RealmsService : IRealmsService
    {
        private readonly GuildCommDbContext context;

        private readonly IMapper mapper;

        public RealmsService(GuildCommDbContext context, IMapper mapper)
        {
            this.context = context;

            this.mapper = mapper;
        }

        public async Task CreateRealmAsync(RealmCreateInputModel inputModel)
        {
            var realm = new Realm
            {
                Name = inputModel.Name,
                RealmType = (RealmType)Enum.Parse(typeof(RealmType), inputModel.RealmType),
                Region = (Region)Enum.Parse(typeof(Region), inputModel.Region)
            };

            await this.context.Realms.AddAsync(realm);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<RealmViewModel>> GetAllRealmViewModelsAsync()
        {
            List<RealmViewModel> realms = await this.context.Realms
                .Select(r => this.mapper.Map<RealmViewModel>(r))
                .ToListAsync();

            return realms;
        }

        public async Task<Realm> GetRealmByNameAsync(string name)
        {
            Realm realm = await this.context.Realms.SingleOrDefaultAsync(dbRealm => dbRealm.Name == name);

            if (realm == null)
            {
                throw new InvalidOperationException("No realm with given name was found");
            }

            return realm;
        }

        public async Task<Realm> GetRealmByIdAsync(int id)
        {
            Realm realm = await this.context.Realms.SingleOrDefaultAsync(dbRealm => dbRealm.Id == id);

            if (realm == null)
            {
                throw new InvalidOperationException("No realm with given Id was found");
            }

            return realm;
        }
    }
}
