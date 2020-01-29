namespace GuildComm.Web.ViewModels
{
    using GuildComm.Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateGuildBindingModel
    {
        public CreateGuildBindingModel()
        {
            Realms = new List<Realm>();
        }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Realm")]
        public string Realm { get; set; }

        public List<Realm> Realms { get; set; }
    }
}
