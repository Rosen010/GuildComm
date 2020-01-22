namespace GuildComm.Data.Configurations
{
    using GuildComm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder
                .HasOne(e => e.Guild)
                .WithMany(g => g.Events)
                .HasForeignKey(e => e.GuildId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
