using GuildComm.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GuildComm.Data
{
    public class GuildCommDbContext : DbContext
    {
        public GuildCommDbContext(DbContextOptions<GuildCommDbContext> options)
            : base(options)
        {

        }

        public DbSet<Realm> Realms { get; set; }
    }
}