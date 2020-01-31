namespace GuildComm.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    using System.Collections.Generic;

    public class GuildCommUser : IdentityUser
    {
        public GuildCommUser()
        {
            this.Characters = new HashSet<Character>();
            this.Applications = new HashSet<Application>();
        }

        public virtual ICollection<Character> Characters { get; set; }

        public virtual ICollection<Application> Applications { get; set; }

        public bool IsInGuild { get; set; }

        public string Description { get; set; }

        public string MemberId { get; set; }
        
        public virtual Member Member { get; set; }
    }
}
