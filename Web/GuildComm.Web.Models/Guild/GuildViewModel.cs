using GuildComm.Web.Models.Items;
using System.Collections.Generic;

namespace GuildComm.Web.Models.Guild
{
    public class GuildViewModel
    {
        public string Name { get; set; }

        public string Realm { get; set; }

        public int MembersCount { get; set; }

        public string NameSpace { get; set; }

        public string Locale { get; set; }

        public int CurrentPage { get; set; }

        public string DisablePrevButton { get; set; }

        public string DisableNextButton { get; set; }

        public ICollection<MemberItem> Members { get; set; }
    }
}
