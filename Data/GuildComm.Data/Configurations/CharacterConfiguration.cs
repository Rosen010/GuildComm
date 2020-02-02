namespace GuildComm.Data.Configurations
{
    using GuildComm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder
                .HasOne(c => c.User)
                .WithMany(u => u.Characters)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.Guild)
                .WithMany(g => g.Characters)
                .HasForeignKey(c => c.GuildId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.Realm)
                .WithMany(r => r.Characters)
                .HasForeignKey(c => c.RealmId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.Member)
                .WithOne(m => m.Character)
                .HasForeignKey<Member>(m => m.CharacterId);
        }
    }
}
