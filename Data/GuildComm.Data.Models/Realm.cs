namespace GuildComm.Data.Models
{
    using Enums;

    using System.Collections.Generic;

    public class Realm
    {
        public Realm()
        {
            this.Guilds = new HashSet<Guild>();
            this.Characters = new HashSet<Character>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Region Region { get; set; }

        public RealmType RealmType { get; set; }

        public virtual ICollection<Character> Characters { get; set; }

        public virtual ICollection<Guild> Guilds { get; set; }
    }
}
