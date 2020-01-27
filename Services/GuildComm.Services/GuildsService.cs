namespace GuildComm.Services
{
    using GuildComm.Data;
    using GuildComm.Data.Models;

    using System.Linq;
    using System.Threading.Tasks;

    public class GuildsService : IGuildsService
    {
        private readonly GuildCommDbContext context;

        public GuildsService(GuildCommDbContext context)
        {
            this.context = context;
        }

        public void CreateGuild(Guild guild)
        {
            this.context.Guilds.Add(guild);
            this.context.SaveChanges();
        }

        public Guild GetGuild(string name)
        {
            var guild = this.context.Guilds.FirstOrDefault(dbGuild => dbGuild.Name == name);
            return guild;
        }
    }
}
