namespace GuildComm.Data.Models
{
    using Enums;
    using System;

    public class Event
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public EventType EventType { get; set; }

        public string Description { get; set; }
    }
}
