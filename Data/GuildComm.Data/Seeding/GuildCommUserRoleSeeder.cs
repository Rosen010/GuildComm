namespace GuildComm.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;

    using System.Linq;
    using System.Threading.Tasks;

    public class GuildCommUserRoleSeeder : ISeeder
    {
        private readonly GuildCommDbContext context;

        public GuildCommUserRoleSeeder(GuildCommDbContext context)
        {
            this.context = context;
        }

        public async Task Seed()
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });

                context.Roles.Add(new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                });
            }

           await context.SaveChangesAsync();
        }
    }
}
