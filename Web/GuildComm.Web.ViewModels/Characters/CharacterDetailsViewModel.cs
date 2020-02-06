namespace GuildComm.Web.ViewModels.Characters
{
    using GuildComm.Data.Models;

    public class CharacterDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public int ItemLevel { get; set; }

        public string Class { get; set; }

        public string Role { get; set; }

        public string GuildName { get; set; }

        public string GuildRegion { get; set; }
    }
}
