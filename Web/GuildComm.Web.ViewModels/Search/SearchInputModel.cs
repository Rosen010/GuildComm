using System.ComponentModel.DataAnnotations;

namespace GuildComm.Web.Models.Search
{
    public class SearchInputModel
    {
        [Required]
        public string Realm { get; set; }

        [Required]
        public string GuildName { get; set; }

        [Required]
        public string Namespace { get; set; }
    }
}
