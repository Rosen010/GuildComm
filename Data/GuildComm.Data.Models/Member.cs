namespace GuildComm.Data.Models
{
    using Enums;

    using System;
    using System.Collections.Generic;

    public class Member
    {
        public Member()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Events = new HashSet<EventParticipant>();
        }

        public string Id { get; set; }

        public int CharacterId { get; set; }

        public virtual Character Character { get; set; }

        public virtual ICollection<EventParticipant> Events { get; set; }

        public Rank Rank { get; set; }

        public DateTime MemberSince { get; set; }

        public string Info { get; set; }

        public string GuildId { get; set; }

        public virtual Guild Guild { get; set; }
    }
}
