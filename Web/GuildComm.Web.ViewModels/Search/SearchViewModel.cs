using GuildComm.Web.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuildComm.Web.Models.Search
{
    public class SearchViewModel : IPaginationModel
    {
        [Required(ErrorMessage = "Please select a realm")]
        public string Realm { get; set; }

        [Required(ErrorMessage = "Please type a guild name")]
        public string GuildName { get; set; }

        [Required(ErrorMessage = "Please select a region")]
        public string Namespace { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<string> Realms { get; set; }
    }
}
