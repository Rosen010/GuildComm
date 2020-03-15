using GuildComm.Data.Models;
using GuildComm.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GuildComm.Data.Seeding
{
    public class GuildCommUserCharacterSeeder : ISeeder
    {
        private readonly GuildCommDbContext context;

        public GuildCommUserCharacterSeeder(GuildCommDbContext context)
        {
            this.context = context;
        }

        public async Task Seed()
        {
            if (!this.context.Characters.Any())
            {
                var firstUser = await this.context.Users.SingleOrDefaultAsync(u => u.UserName == "Pesho");

                var firstUserCharacter = new Character
                {
                    Name = "Nexxus",
                    Role = Role.Healer,
                    Class = Class.Paladin,
                    Level = 120,
                    ItemLevel = 460,
                    RealmId = 1,
                    UserId = firstUser.Id
                };

                var secondUser = await this.context.Users.SingleOrDefaultAsync(u => u.UserName == "Gosho");

                var secondUserCharacter = new Character
                {
                    Name = "Feugen",
                    Role = Role.Tank,
                    Class = Class.Warrior,
                    Level = 120,
                    ItemLevel = 460,
                    RealmId = 1,
                    UserId = secondUser.Id
                };

                var thirdUser = await this.context.Users.SingleOrDefaultAsync(u => u.UserName == "Kiro");

                var thirdUserCharacter = new Character
                {
                    Name = "Josh",
                    Role = Role.DPS,
                    Class = Class.Hunter,
                    Level = 120,
                    ItemLevel = 460,
                    RealmId = 1,
                    UserId = thirdUser.Id
                };

                context.Characters.AddRange(new Character[] { firstUserCharacter, secondUserCharacter, thirdUserCharacter });
                await context.SaveChangesAsync();
            }           
        }
    }
}
