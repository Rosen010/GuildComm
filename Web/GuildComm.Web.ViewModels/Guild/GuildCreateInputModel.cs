namespace GuildComm.Web.ViewModels
{
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels.Realms;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GuildCreateInputModel
    {
        public GuildCreateInputModel()
        {
            this.Realms = new List<RealmViewModel>();
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

        public int RealmId { get; set; }

        public List<RealmViewModel> Realms { get; set; }

        public Character Character { get; set; }

        public List<Character> Characters { get; set; }
    }
}
