namespace Guild.Domain
{
    using System.Collections.Generic;
    public class Guild
    {
        public string Name { get; set; }

        public ICollection<Member> Members { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<Character> Characters { get; set; }
    }
}
