namespace GuildComm.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class GuildCommUser : IdentityUser
    {
        public GuildCommUser()
        {

        }
   
        public string Description { get; set; }

    }
}
