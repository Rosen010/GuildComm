using Newtonsoft.Json;

namespace GuildComm.Services.Models.ResponseModels.ResponseComponents
{
    public class RealmComponent
    {
        [JsonProperty("key")]
        public KeyComponent Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
    }
}
