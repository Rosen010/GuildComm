namespace GuildComm.Data
{
    using GuildComm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class GuildCommDbContext : IdentityDbContext<GuildCommUser, IdentityRole, string>
    {
        public GuildCommDbContext(DbContextOptions<GuildCommDbContext> options) : base(options)
        {

        }
    }
}
