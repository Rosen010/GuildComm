﻿namespace GuildComm.Data.Models
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

        public Realm Realm { get; set; }

        public ICollection<Member> Members { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<Character> Characters { get; set; }

        public ICollection<Application> Applications { get; set; }
    }
}
