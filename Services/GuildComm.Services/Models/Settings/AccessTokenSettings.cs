using GuildComm.Services.Contracts;

namespace GuildComm.Services.Models.Settings
{
    public class AccessTokenSettings : ISettings
    {
        public string Token { get; set; }

        public string Expires { get; set; }
    }
}
