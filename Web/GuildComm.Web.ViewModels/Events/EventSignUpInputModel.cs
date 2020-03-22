using GuildComm.Web.ViewModels.Characters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuildComm.Web.ViewModels.Events
{
    public class EventSignUpInputModel
    {
        [Required]
        [Display(Name = "Character")]
        public string Character { get; set; }

        public List<CharacterViewModel> Characters { get; set; }

        public int EventId { get; set; }

        public string GuildId { get; set; }
    }
}
