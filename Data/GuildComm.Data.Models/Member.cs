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

        public string Name { get; set; }

        public ICollection<Character> Characters { get; set; }

        public Rank Rank { get; set; }

        public DateTime MemberSince { get; set; }

        public string Info { get; set; }
    }
}
