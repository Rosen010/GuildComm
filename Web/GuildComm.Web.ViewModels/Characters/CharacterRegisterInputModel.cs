namespace GuildComm.Web.ViewModels.Characters
{
    using GuildComm.Data.Models;
    using GuildComm.Data.Models.Enums;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CharacterRegisterInputModel
    {
        public CharacterRegisterInputModel()
        {
            this.Realms = new List<Realm>();
        }

        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Role")]
        public Role Role { get; set; }

        [Required]
        [Display(Name = "Class")]
        public Class Class { get; set; }

        [Required]
        [Range(1, 120)]
        [Display(Name = "Level")]
        public int Level { get; set; }

        [Required]
        [Range(0, 500)]
        [Display(Name = "ItemLevel")]
        public int ItemLevel { get; set; }

        [Required]
        [Range(0, 500)]
        [Display(Name = "RealmName")]
        public string RealmName { get; set; }

        public Realm Realm { get; set; }

        public List<Realm> Realms { get; set; }

        public GuildCommUser User { get; set; }
    }
}
