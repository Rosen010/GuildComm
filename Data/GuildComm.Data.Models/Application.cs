namespace GuildComm.Data.Models
{
    using GuildComm.Data.Models.Enums;

    using System;

    public class Application
    {
        public int Id { get; set; }

        public string CharacterName { get; set; }

        public int? Age { get; set; }

        public string Country { get; set; }

        public Role Role { get; set; }

        public string Experience { get; set; }

        public string ArmoryLink { get; set; }

        public string GuildId { get; set; }

        public virtual Guild Guild { get; set; }

        public int CharacterId { get; set; }

        public virtual Character Character { get; set; }
    }
}
