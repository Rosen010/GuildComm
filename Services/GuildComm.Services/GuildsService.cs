namespace GuildComm.Services
{
    using GuildComm.Data;
    using GuildComm.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class GuildsService : IGuildsService
    {
        private readonly GuildCommDbContext context;

        public GuildsService(GuildCommDbContext context)
        {
            this.context = context;
        }

        public async Task CreateGuildAsync(Guild guild)
        {
            await this.context.Guilds.AddAsync(guild);
            await this.context.SaveChangesAsync();
        }

        public async Task<Guild> GetGuildAsync(string name)
        {
            var guild = await this.context.Guilds.SingleOrDefaultAsync(dbGuild => dbGuild.Name == name);
            return guild;
        }
    }
}
