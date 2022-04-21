using GuildComm.Common.Constants;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuildComm.Web.Models.Character
{
    public class CharacterInputModel
    {
        [Required(ErrorMessage = ErrorMessages.SelectRealmMessage)]
        public string Realm { get; set; }

        [Required(ErrorMessage = ErrorMessages.TypeCharacterNameMessage)]
        public string CharacterName { get; set; }

        [Required(ErrorMessage = ErrorMessages.SelectRegionMessage)]
        public string NameSpace { get; set; }

        public IEnumerable<string> Realms { get; set; }
    }
}
