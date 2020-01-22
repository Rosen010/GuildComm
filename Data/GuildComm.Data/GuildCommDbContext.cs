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

        public DbSet<Character> Characters { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Guild> Guilds { get; set; }

        public DbSet<Member> Members { get; set; }
    }
}
