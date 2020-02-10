namespace GuildComm.Web.ViewModels
{
    using GuildComm.Web.ViewModels.Realms;
    using GuildComm.Web.ViewModels.Characters;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GuildCreateInputModel
    {
        public GuildCreateInputModel()
        {
            this.Realms = new List<RealmViewModel>();
            this.Characters = new List<CharacterViewModel>();
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
        
        public List<CharacterViewModel> Characters { get; set; }
    }
}
