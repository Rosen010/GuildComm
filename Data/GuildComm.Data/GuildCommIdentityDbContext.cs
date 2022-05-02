using GuildComm.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GuildComm.Data
{
    public class GuildCommIdentityDbContext : IdentityDbContext<GuildCommUser, IdentityRole, string>
    {
        public GuildCommIdentityDbContext(DbContextOptions<GuildCommIdentityDbContext> options)
            : base(options)
        {

        }
    }
}
