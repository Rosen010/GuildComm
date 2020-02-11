namespace GuildComm.Web.ViewModels.Users
{
    using System.Collections.Generic;
    using GuildComm.Web.ViewModels.Characters;

    public class GuildCommUserDetailsViewModel
    {
        public GuildCommUserDetailsViewModel()
        {
            this.Characters = new List<CharacterViewModel>();
            this.Guilds = new List<GuildsAllViewModel>();
        }

        public string Username { get; set; }

        public string Description { get; set; }

        public List<CharacterViewModel> Characters { get; set; }

        public List<GuildsAllViewModel> Guilds { get; set; }
    }
}
