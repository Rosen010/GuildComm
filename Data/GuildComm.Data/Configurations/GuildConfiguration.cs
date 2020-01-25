namespace GuildComm.Data.Configurations
{
    using GuildComm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GuildConfiguration : IEntityTypeConfiguration<Guild>
    {
        public void Configure(EntityTypeBuilder<Guild> builder)
        {
            builder
                .HasOne(g => g.Realm)
                .WithMany(r => r.Guilds)
                .HasForeignKey(g => g.RealmId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
