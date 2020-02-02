namespace GuildComm.Data.Configurations
{
    using GuildComm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder
                .HasOne(m => m.Guild)
                .WithMany(g => g.Members)
                .HasForeignKey(m => m.GuildId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
