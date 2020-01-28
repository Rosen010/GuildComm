namespace GuildComm.Web.ViewModels
{
    using GuildComm.Data.Models;

    public class GuildsAllViewModel
    {
        public string Name { get; set; }

        public Realm Realm { get; set; }

        public int MembersCount { get; set; }
    }
}
