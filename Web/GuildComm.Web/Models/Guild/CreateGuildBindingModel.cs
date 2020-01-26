namespace GuildComm.Web.Models.Guild
{
    using System.ComponentModel.DataAnnotations;

    public class CreateGuildBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Realm { get; set; }
    }
}
