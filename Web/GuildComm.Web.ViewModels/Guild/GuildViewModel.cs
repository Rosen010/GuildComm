using GuildComm.Web.Models.Items;
using System.Collections.Generic;

namespace GuildComm.Web.Models.Guild
{
    public class GuildViewModel
    {
        public string Name { get; set; }

        public string Realm { get; set; }

        public int MembersCount { get; set; }

        public ICollection<MemberItem> Members { get; set; }
    }
}
