using GuildComm.Data.Models;

namespace GuildComm.Web.Models.Guild
{
    public class GuildsAllViewModel
    {
        public string Name { get; set; }

        public Realm Realm { get; set; }

        public int MembersCount { get; set; }
    }
}
