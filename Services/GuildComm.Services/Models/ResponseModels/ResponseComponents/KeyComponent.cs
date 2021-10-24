using Newtonsoft.Json;

namespace GuildComm.Services.Models.ResponseModels.ResponseComponents
{
    public class KeyComponent
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
