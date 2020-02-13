﻿using System.ComponentModel.DataAnnotations;

namespace GuildComm.Web.ViewModels.Applications
{
    class ApplicationCreateInputModel
    {
        [Required]
        [MaxLength(20)]
        public string CharacterName { get; set; }

        public int? Age { get; set; }

        [Required]
        [MaxLength(20)]
        public string Country { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; }

        [Required]
        public string Experience { get; set; }

        [Required]
        public string ArmoryLink { get; set; }

        public string UserId { get; set; }

        public string GuildId { get; set; }

    }
}