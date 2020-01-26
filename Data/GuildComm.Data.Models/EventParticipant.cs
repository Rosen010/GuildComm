namespace GuildComm.Data.Models
{
    public class EventParticipant
    {
        public string ParticipantId { get; set; }

        public Member Participant { get; set; }

        public string EventId { get; set; }

        public Event Event { get; set; }
    }
}
