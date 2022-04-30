using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GuildComm.Data
{
    public class GuildCommIdentityDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public GuildCommIdentityDbContext(DbContextOptions<GuildCommIdentityDbContext> options)
            : base(options)
        {

        }
    }
}
