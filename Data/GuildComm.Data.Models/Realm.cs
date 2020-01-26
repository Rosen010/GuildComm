namespace GuildComm.Data.Models
{
    using Enums;

    using System;
    using System.Collections.Generic;

    public class Realm
    {
        public Realm()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Guilds = new HashSet<Guild>();
            this.Users = new HashSet<GuildCommUser>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public Region Region { get; set; }

        public RealmType RealmType { get; set; }

        public ICollection<Guild> Guilds { get; set; }

        public ICollection<GuildCommUser> Users { get; set; }
    }
}
