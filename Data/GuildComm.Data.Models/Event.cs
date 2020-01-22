namespace GuildComm.Data.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;

    public class Event
    {
        public Event()
        {
            this.Participants = new HashSet<GuildEvent>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public EventType EventType { get; set; }

        public string Description { get; set; }

        public ICollection<GuildEvent> Participants { get; set; }

        public string GuildId { get; set; }

        public Guild Guild { get; set; }
    }
}
