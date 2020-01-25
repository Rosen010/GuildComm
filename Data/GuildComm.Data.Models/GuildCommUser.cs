namespace GuildComm.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class GuildCommUser : IdentityUser
    {
        public GuildCommUser()
        {
            this.Characters = new HashSet<Character>();
        }

        public ICollection<Character> Characters { get; set; }

        public ICollection<Application> Applications { get; set; }

        public bool IsInGuild { get; set; }

        public string Description { get; set; }

        public string RealmId { get; set; }

        public Realm Realm { get; set; }

        public string MemberId { get; set; }

        public Member Member { get; set; }
    }
}
