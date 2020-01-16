namespace Guild.Domain
{
    using Guild.Domain.Enums;
    using System;
    using System.Collections.Generic;

    public class Member
    {
        public string Name { get; set; }

        public ICollection<Character> Characters { get; set; }

        public Rank Rank { get; set; }

        public DateTime MemberSince { get; set; }

        public string Info { get; set; }
    }
}
