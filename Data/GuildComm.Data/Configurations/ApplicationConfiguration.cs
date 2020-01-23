namespace GuildComm.Data.Configurations
{
    using GuildComm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder
                .HasOne(a => a.Guild)
                .WithMany(g => g.Applications)
                .HasForeignKey(a => a.GuildId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(a => a.User)
                .WithMany(u => u.Applications)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
