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

    public class RealmsService : IRealmsService
    {
        private readonly GuildCommDbContext context;

        private readonly IMapper mapper;

        public RealmsService(GuildCommDbContext context, IMapper mapper)
        {
            this.context = context;

            this.mapper = mapper;
        }

        public async Task<List<Realm>> GetAllRealmsAsync()
        {
            List<Realm> realms = await this.context.Realms.ToListAsync();
            return realms;
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

            return realm;
        }

        public async Task<Realm> GetRealmByIdAsync(int id)
        {
            Realm realm = await this.context.Realms.SingleOrDefaultAsync(dbRealm => dbRealm.Id == id);

            return realm;
        }
    }
}
