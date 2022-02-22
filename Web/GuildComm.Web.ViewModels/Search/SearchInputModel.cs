using GuildComm.Web.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GuildComm.Web.Models.Search
{
    public class SearchInputModel : IPaginationModel
    {
        [Required(ErrorMessage = "Please select a realm")]
        public string Realm { get; set; }

        [Required(ErrorMessage = "Please type a guild name")]
        public string GuildName { get; set; }

        [Required(ErrorMessage = "Please select a region")]
        public string Namespace { get; set; }

        public int CurrentPage { get; set; }
    }
}
