﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GuildComm.Web.ViewModels.Events
{
    public class EventCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string EventType { get; set; }

        [Required]
        public string Description { get; set; }

        public string GuildId { get; set; }
    }
}
