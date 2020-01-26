namespace GuildComm.Data.Seeding
{
    using GuildComm.Data.Models;

    using Newtonsoft.Json;

    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    public class GuildCommRealmSeeder : ISeeder
    {
        private static string currentDirectory = Assembly.GetExecutingAssembly().Location;

        private readonly GuildCommDbContext context;
        private string jsonData = File.ReadAllText(currentDirectory + @"../../../../../../../Data/GuildComm.Data/Seeding/Datasets/Realms.Json");

        public GuildCommRealmSeeder(GuildCommDbContext context)
        {
            this.context = context;
        }

        public async Task Seed()
        {
            var realms = JsonConvert.DeserializeObject<Realm[]>(jsonData);

            await context.Realms.AddRangeAsync(realms);
        }
    }
}
