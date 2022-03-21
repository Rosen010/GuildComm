using System.Collections.Generic;

namespace GuildComm.Web.Models.Character
{
    public class CharacterInputModel
    {
        public string Realm { get; set; }

        public string CharacterName { get; set; }

        public string NameSpace { get; set; }

        public IEnumerable<string> Realms { get; set; }
    }
}
