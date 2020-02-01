namespace GuildComm.Web.ViewModels.Characters
{
    using GuildComm.Data.Models;

    public class CharacterViewModel
    {
        public string Name { get; set; }

        public int Level { get; set; }

        public int ItemLevel { get; set; }

        public string GuildName { get; set; }

        public Realm Realm { get; set; }
    }
}
