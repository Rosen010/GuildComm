namespace GuildComm.Data.Configurations
{
    using GuildComm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GuildEventConfiguration : IEntityTypeConfiguration<EventParticipant>
    {
        public void Configure(EntityTypeBuilder<EventParticipant> builder)
        {
            builder
                .HasKey(ge => new { ge.ParticipantId, ge.EventId });

            builder
                .HasOne(ge => ge.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(ge => ge.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(ge => ge.Participant)
                .WithMany(m => m.Events)
                .HasForeignKey(ge => ge.ParticipantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
