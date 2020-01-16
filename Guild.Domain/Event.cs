using Guild.Domain.Enums;

namespace Guild.Domain
{
    using System;

    public class Event
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public EventType EventType { get; set; }

        public string Description { get; set; }
    }
}
