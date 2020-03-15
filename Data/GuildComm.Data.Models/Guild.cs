namespace GuildComm.Data.Models
{
    using GuildComm.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Guild
    {
        public Guild()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Members = new HashSet<Member>();
            this.Events = new HashSet<Event>();
            this.Characters = new HashSet<Character>();
            this.Applications = new HashSet<Application>();

            this.Ranks = new HashSet<Rank> { Rank.Trial, Rank.Raider, Rank.Officer, Rank.Member, Rank.GuildeMaster };
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public int RealmId { get; set; }

        public virtual Realm Realm { get; set; }

        public string GuildMaster { get; set; }

        public string Information { get; set; }

        [NotMapped]
        public virtual ICollection<Rank> Ranks { get; set; }

        public virtual ICollection<Member> Members { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Character> Characters { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
    }
}