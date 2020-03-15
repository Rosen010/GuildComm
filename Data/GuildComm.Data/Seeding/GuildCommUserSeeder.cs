namespace GuildComm.Data.Seeding
{
    using GuildComm.Common;
    using GuildComm.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Newtonsoft.Json;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class GuildCommUserSeeder : ISeeder
    {
        private readonly GuildCommDbContext context;
        private readonly UserManager<GuildCommUser> _userManager;

        private string jsonData = File.ReadAllText(GlobalConstants.userJsonLocation);

        public GuildCommUserSeeder(GuildCommDbContext context, UserManager<GuildCommUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            if (!context.Users.Any())
            {
                var users = JsonConvert.DeserializeObject<GuildCommUser[]>(jsonData);

                foreach (var user in users)
                {
                    var result = await _userManager.CreateAsync(user, "123123");

                    if (result.Succeeded)
                    {
                        if (_userManager.Users.Count() == 1)
                        {
                            await _userManager.AddToRoleAsync(user, "Admin");
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(user, "User");
                        }
                    }
                }

                await context.Users.AddRangeAsync(users);
            }
        }
    }
}
