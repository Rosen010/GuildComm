namespace GuildComm.Data
{
    using GuildComm.Data.Models;

    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class GuildCommDbContext : IdentityDbContext<GuildCommUser, IdentityRole, string>
    {
        public GuildCommDbContext(DbContextOptions<GuildCommDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
