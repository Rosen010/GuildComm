namespace GuildComm.Data.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;

    public class Member
    {
        public Member()
        {
            this.Characters = new List<Character>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<Character> Characters { get; set; }

        public ICollection<GuildEvent> Events { get; set; }

        public Rank Rank { get; set; }

        public DateTime MemberSince { get; set; }

        public string Info { get; set; }

        public string UserId { get; set; }

        public GuildCommUser User { get; set; }

        public string GuildId { get; set; }

        public Guild Guild { get; set; }
    }
}
