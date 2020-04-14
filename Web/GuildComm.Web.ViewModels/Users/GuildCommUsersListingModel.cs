namespace GuildComm.Web.ViewModels.Users
{
    using System.Collections.Generic;

    public class GuildCommUsersListingModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public List<GuildCommUserViewModel> Users { get; set; }
    }
}