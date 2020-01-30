namespace GuildComm.Web.ViewModels
{
    using GuildComm.Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GuildCreateInputModel
    {
        public GuildCreateInputModel()
        {
            Realms = new List<Realm>();
        }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "RealmName")]
        public string RealmName { get; set; }

        public Realm Realm { get; set; }

        public List<Realm> Realms { get; set; }
    }
}
