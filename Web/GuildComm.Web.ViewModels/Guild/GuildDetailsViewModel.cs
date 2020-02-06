namespace GuildComm.Web.ViewModels.Guild
{
    using System.Collections.Generic;
    using GuildComm.Web.ViewModels.Characters;

    public class GuildDetailsViewModel
    {
        public GuildDetailsViewModel()
        {
            this.Characters = new List<CharacterViewModel>();
        }
        public string Id { get; set; }

        public string Name { get; set; }

        public string RealmName { get; set; }

        public string RealmRegion { get; set; }

        public IList<CharacterViewModel> Characters { get; set; }
    }
}
