using Newtonsoft.Json;

namespace GuildComm.Services.Models.ResponseModels.ResponseComponents
{
    public class FactionComponent
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
