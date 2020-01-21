namespace GuildComm.Domain
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class GuildCommUser : IdentityUser
    {
        public GuildCommUser()
        {
            this.Characters = new HashSet<Character>();
        }

        public string Username { get; set; }

        public ICollection<Character> Characters { get; set; }

        public bool IsInGuild { get; set; }

        public string Description { get; set; }
    }
}
