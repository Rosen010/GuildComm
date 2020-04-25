namespace GuildComm.Web.ViewModels.Realms
{
    using GuildComm.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class RealmCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string RealmType { get; set; }
    }
}
