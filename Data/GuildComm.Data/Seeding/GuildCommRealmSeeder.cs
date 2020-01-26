namespace GuildComm.Data.Seeding
{
    using GuildComm.Common;
    using GuildComm.Data.Models;

    using Newtonsoft.Json;

    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class GuildCommRealmSeeder : ISeeder
    {
        private readonly GuildCommDbContext context;
        private string jsonData = File.ReadAllText(GlobalConstants.realmJsonLocation);

        public GuildCommRealmSeeder(GuildCommDbContext context)
        {
            this.context = context;
        }

        public async Task Seed()
        {
            if (!context.Realms.Any())
            {
                var realms = JsonConvert.DeserializeObject<Realm[]>(jsonData);

                await context.Realms.AddRangeAsync(realms);
            }            
        }
    }
}
