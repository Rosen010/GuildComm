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

        public virtual ICollection<Character> Characters { get; set; }
      
        public string Description { get; set; }

        public string GuildId { get; set; }

        public Guild Guild { get; set; }
    }
}
