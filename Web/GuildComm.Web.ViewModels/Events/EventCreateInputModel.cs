using System;

namespace GuildComm.Web.ViewModels.Events
{
    public class EventCreateInputModel
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string EventType { get; set; }

        public string Description { get; set; }

        public string GuildId { get; set; }
    }
}
