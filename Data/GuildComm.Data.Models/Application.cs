using GuildComm.Data.Models.Enums;

namespace GuildComm.Data.Models
{
    public class Application
    {
        public string Id { get; set; }

        public string MainCharacterName { get; set; }

        public int? Age { get; set; }

        public string Country { get; set; }

        public Role Role { get; set; }

        public string Experience { get; set; }

        public string UserId { get; set; }

        public GuildCommUser User { get; set; }

        public string GuildId { get; set; }

        public Guild Guild { get; set; }
    }
}
