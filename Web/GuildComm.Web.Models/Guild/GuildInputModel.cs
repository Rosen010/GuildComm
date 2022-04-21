using GuildComm.Common.Constants;
using GuildComm.Web.Models.Interfaces;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuildComm.Web.Models.Guild
{
    public class GuildInputModel : IPaginationModel
    {
        [Required(ErrorMessage = ErrorMessages.SelectRealmMessage)]
        public string Realm { get; set; }

        [Required(ErrorMessage = ErrorMessages.TypeGuildNameMessage)]
        public string GuildName { get; set; }

        [Required(ErrorMessage = ErrorMessages.SelectRegionMessage)]
        public string Namespace { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<string> Realms { get; set; }
    }
}
