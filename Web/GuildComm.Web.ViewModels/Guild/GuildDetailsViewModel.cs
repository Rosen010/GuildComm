namespace GuildComm.Web.ViewModels.Guild
{
    using System.Collections.Generic;
    using GuildComm.Web.ViewModels.Members;

    public class GuildDetailsViewModel
    {
        public GuildDetailsViewModel()
        {
            this.Members = new List<MemberViewModel>();
            this.UserCharacters = new List<MemberViewModel>();
        }
        public string Id { get; set; }

        public string Name { get; set; }

        public string RealmName { get; set; }

        public string RealmRegion { get; set; }

        public IList<MemberViewModel> Members { get; set; }

        public IList<MemberViewModel> UserCharacters { get; set; }
    }
}
