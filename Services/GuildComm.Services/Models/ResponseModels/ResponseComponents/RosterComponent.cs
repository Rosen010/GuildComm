using Newtonsoft.Json;

namespace GuildComm.Services.Models.ResponseModels.ResponseComponents
{
    public class RosterComponent
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
