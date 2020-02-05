using GuildComm.Data.Models;
using GuildComm.Web.ViewModels.Characters;
using System.Collections.Generic;

namespace GuildComm.Web.ViewModels.Guild
{
    public class GuildDetailsViewModel
    {
        public GuildDetailsViewModel()
        {
            this.Characters = new List<CharacterViewModel>();
        }

        public string Name { get; set; }

        public string RealmName { get; set; }

        public ICollection<CharacterViewModel> Characters { get; set; }

        public Realm Realm { get; set; }
    }
}
