namespace GuildComm.Web.ViewModels.Users
{
    using GuildComm.Data.Models;

    public class GuildCommUserDetailsViewModel
    {
        public string Username { get; set; }

        public bool IsInGuild { get; set; }

        public string Description { get; set; }

        public Realm Realm { get; set; }
    }
}
