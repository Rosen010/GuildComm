using AutoMapper;
using GuildComm.Data.Models;
using GuildComm.Data.Models.Enums;
using GuildComm.Web.ViewModels;
using GuildComm.Web.ViewModels.Characters;
using GuildComm.Web.ViewModels.Events;
using GuildComm.Web.ViewModels.Guild;
using GuildComm.Web.ViewModels.Members;
using GuildComm.Web.ViewModels.Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuildComm.Services.Utilities
{
    public class TestiningProfile : Profile
    {
        public TestiningProfile()
        {
            this.CreateMap<Guild, GuildsAllViewModel>()
               .ForMember(x => x.MembersCount, y => y.MapFrom(s => s.Members.Count()));

            this.CreateMap<Guild, GuildDetailsViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name))
                .ForMember(x => x.RealmName, y => y.MapFrom(s => s.Realm.Name))
                .ForMember(x => x.RealmRegion, y => y.MapFrom(s => s.Realm.Region.ToString()));

            this.CreateMap<Guild, GuildManageViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name))
                .ForMember(x => x.RealmName, y => y.MapFrom(s => s.Realm.Name))
                .ForMember(x => x.RealmRegion, y => y.MapFrom(s => s.Realm.Region.ToString()));

            this.CreateMap<Member, MemberViewModel>()
                .ForMember(x => x.MemberSince, y => y.MapFrom(s => s.MemberSince.Date.ToString("dd/MM/yyyy")));

            this.CreateMap<CharacterRegisterInputModel, Character>();

            this.CreateMap<Character, CharacterViewModel>()
                .ForMember(x => x.GuildName, y => y.MapFrom(s => s.Guild != null ? s.Guild.Name : "N/A"))
                .ForMember(x => x.RealmName, y => y.MapFrom(s => s.Realm.Name))
                .ForMember(x => x.RealmRegion, y => y.MapFrom(s => s.Realm.Region.ToString()));

            this.CreateMap<Character, CharacterDetailsViewModel>()
                .ForMember(x => x.Class, y => y.MapFrom(s => s.Class.ToString()))
                .ForMember(x => x.Role, y => y.MapFrom(s => s.Role.ToString()))
                .ForMember(x => x.GuildName, y => y.MapFrom(s => s.Guild != null ? s.Guild.Name : "N/A"))
                .ForMember(x => x.GuildRegion, y => y.MapFrom(s => s.Guild != null ? s.Guild.Realm.Region.ToString() : "N/A"));

            this.CreateMap<Realm, RealmViewModel>()
              .ForMember(x => x.Type, y => y.MapFrom(s => s.RealmType.ToString()));

            this.CreateMap<Event, EventsAllViewModel>()
               .ForMember(x => x.EvenType, y => y.MapFrom(s => s.EventType.ToString()))
               .ForMember(x => x.ParticipantsCount, y => y.MapFrom(s => s.Participants.Count));

            this.CreateMap<EventCreateInputModel, Event>()
                .ForMember(x => x.EventType, y => y.MapFrom(s => (EventType)(Enum.Parse(typeof(EventType), s.EventType))));
        }
    }
}
