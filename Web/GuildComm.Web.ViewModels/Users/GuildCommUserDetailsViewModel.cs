namespace GuildComm.Web.ViewModels.Users
{
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels.Characters;
    using System.Collections.Generic;

    public class GuildCommUserDetailsViewModel
    {
        public GuildCommUserDetailsViewModel()
        {
            this.Characters = new List<CharacterViewModel>();
        }

        public string Username { get; set; }

        public bool IsInGuild { get; set; }

        public string GuildName { get; set; }

        public string Description { get; set; }

        public List<CharacterViewModel> Characters { get; set; }
    }
}
