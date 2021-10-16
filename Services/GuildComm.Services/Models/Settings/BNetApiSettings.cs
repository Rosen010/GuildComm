using GuildComm.Services.Contracts;

namespace GuildComm.Services.Models.Settings
{
    public class BNetApiSettings : ISettings
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }
}
