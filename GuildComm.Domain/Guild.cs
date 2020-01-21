namespace GuildComm.Domain
{
    using System.Collections.Generic;

    public class Guild
    {
        public Guild()
        {
            this.Members = new HashSet<Member>();
            this.Events = new HashSet<Event>();
            this.Characters = new HashSet<Character>();
        }

        public string Name { get; set; }

        public ICollection<Member> Members { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<Character> Characters { get; set; }
    }
}
