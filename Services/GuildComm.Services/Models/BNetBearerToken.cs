using Newtonsoft.Json;

namespace GuildComm.Services.Models
{
    public class BNetBearerToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int Expiration { get; set; }
    }
}
