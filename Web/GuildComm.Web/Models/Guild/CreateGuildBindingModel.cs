namespace GuildComm.Web.Models.Guild
{
    using System.ComponentModel.DataAnnotations;

    public class CreateGuildBindingModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Realm")]
        public string Realm { get; set; }
    }
}
