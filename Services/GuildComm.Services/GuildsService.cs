namespace GuildComm.Services
{
    using AutoMapper;
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels;

    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class GuildsService : IGuildsService
    {
        private readonly GuildCommDbContext context;
        private readonly IRealmsService realmsService;

        private readonly IMapper mapper;

        public GuildsService(GuildCommDbContext context, IRealmsService realmsService, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

            this.realmsService = realmsService;
        }

        public async Task CreateGuildAsync(GuildCreateInputModel inputModel)
        {
            inputModel.Realm = await realmsService.GetRealmByNameAsync(inputModel.RealmName);
            Guild guild = this.mapper.Map<Guild>(inputModel);

            await this.context.Guilds.AddAsync(guild);
            await this.context.SaveChangesAsync();
        }

        public async Task<Guild> GetGuildAsync(string name)
        {
            var guild = await this.context.Guilds
                .SingleOrDefaultAsync(dbGuild => dbGuild.Name == name);
            return guild;
        }

        public async Task<List<GuildsAllViewModel>> GetAllGuildsAsync()
        {
            List<GuildsAllViewModel> guilds = new List<GuildsAllViewModel>();

            var guildsFromDb = await this.context.Guilds
                .Include(g => g.Realm)
                .ToListAsync();

            foreach (var guild in guildsFromDb)
            {
                var guildToAdd = this.mapper.Map<GuildsAllViewModel>(guild);
                guilds.Add(guildToAdd);
            }

            return guilds;
        }
    }
}
