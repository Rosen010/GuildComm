namespace GuildComm.Data.Models
{
    using Enums;

    public class Character
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Role Role { get; set; }

        public Class Class { get; set; }

        public int Level { get; set; }

        public int ItemLevel { get; set; }

        public string GuildId { get; set; }

        public Guild Guild { get; set; }

        public string MemberId { get; set; }

        public Member Member { get; set; }

        public string UserId { get; set; }

        public GuildCommUser User { get; set; }
    }
}