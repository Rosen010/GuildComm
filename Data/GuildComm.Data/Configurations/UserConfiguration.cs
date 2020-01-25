namespace GuildComm.Data.Configurations
{
    using GuildComm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<GuildCommUser>
    {
        public void Configure(EntityTypeBuilder<GuildCommUser> builder)
        {
            builder
                .HasOne(u => u.Realm)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RealmId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
