namespace GuildComm.Web.ViewModels.Guild
{
    using System.Collections.Generic;

    public class GuildsListingModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public List<GuildsAllViewModel> Guilds { get; set; }
    }
}
