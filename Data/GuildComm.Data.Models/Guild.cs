namespace GuildComm.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Guild
    {
        public Guild()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Members = new HashSet<Member>();
            this.Events = new HashSet<Event>();
            this.Characters = new HashSet<Character>();
            this.Applications = new HashSet<Application>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string RealmId { get; set; }

        public virtual Realm Realm { get; set; }

        public virtual ICollection<Member> Members { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Character> Characters { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
    }
}
