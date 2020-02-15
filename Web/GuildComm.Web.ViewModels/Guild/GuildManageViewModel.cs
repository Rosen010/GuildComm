using GuildComm.Web.ViewModels.Members;
using System.Collections.Generic;

namespace GuildComm.Web.ViewModels.Guild
{
    public class GuildManageViewModel
    {
        public GuildManageViewModel()
        {
            this.Members = new List<MemberViewModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string RealmName { get; set; }

        public string RealmRegion { get; set; }

        public IList<MemberViewModel> Members { get; set; }
    }
}
