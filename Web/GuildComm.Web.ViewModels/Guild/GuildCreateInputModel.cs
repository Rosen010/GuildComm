namespace GuildComm.Web.ViewModels
{
    using GuildComm.Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GuildCreateInputModel
    {
        public GuildCreateInputModel()
        {
            this.Realms = new List<Realm>();
            this.Characters = new List<Character>();
        }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "RealmName")]
        public string RealmName { get; set; }

        [Required]
        public string MasterCharacter { get; set; }

        public Realm Realm { get; set; }

        public List<Realm> Realms { get; set; }

        public Character Character { get; set; }

        public List<Character> Characters { get; set; }
    }
}
