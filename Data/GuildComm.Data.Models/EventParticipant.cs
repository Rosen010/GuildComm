namespace GuildComm.Data.Models
{
    public class EventParticipant
    {
        public string ParticipantId { get; set; }

        public virtual Member Participant { get; set; }

        public string EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
