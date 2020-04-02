namespace GuildComm.Services.Utilities
{
    using System;
    using AutoMapper;

    using GuildComm.Data.Models;
    using GuildComm.Data.Models.Enums;
    using GuildComm.Web.ViewModels.Events;

    public class EventProfile : Profile
    {
        public EventProfile()
        {
            this.CreateMap<Event, EventsAllViewModel>()
                .ForMember(x => x.EvenType, y => y.MapFrom(s => s.EventType.ToString()))
                .ForMember(x => x.ParticipantsCount, y => y.MapFrom(s => s.Participants.Count));

            this.CreateMap<EventCreateInputModel, Event>()
                .ForMember(x => x.EventType, y => y.MapFrom(s => (EventType)(Enum.Parse(typeof(EventType), s.EventType))));
        }
    }
}
