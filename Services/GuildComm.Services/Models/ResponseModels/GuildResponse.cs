using GuildComm.Services.Models.ResponseModels.ResponseComponents;
using Newtonsoft.Json;

namespace GuildComm.Services.Models.ResponseModels
{
    public class GuildResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("member_count")]
        public int MembersCount { get; set; }

        [JsonProperty("realm")]
        public RealmComponent Realm { get; set; }

        [JsonProperty("roster")]
        public RosterComponent RosterUrl { get; set; }

        [JsonProperty("created_timestamp")]
        public long CreatedOn { get; set; }
    }
}
