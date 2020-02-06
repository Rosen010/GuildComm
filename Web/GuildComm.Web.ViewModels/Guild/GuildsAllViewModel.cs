namespace GuildComm.Web.ViewModels
{
    using GuildComm.Data.Models;

    public class GuildsAllViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Realm Realm { get; set; }

        public int MembersCount { get; set; }
    }
}