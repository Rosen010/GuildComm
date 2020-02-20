namespace GuildComm.Services.Data.Utilities
{
    using AutoMapper;
    using System.Linq;

    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels;
    using GuildComm.Web.ViewModels.Characters;
    using GuildComm.Web.ViewModels.Users;
    using GuildComm.Web.ViewModels.Guild;
    using GuildComm.Web.ViewModels.Realms;
    using GuildComm.Web.ViewModels.Members;
    using GuildComm.Web.ViewModels.Applications;
    using GuildComm.Data.Models.Enums;
    using System;

    public class GuildCommProfile : Profile
    {
        public GuildCommProfile()
        {
            // Guild
            //this.CreateMap<GuildCreateInputModel, Guild>()
            //    .ForMember(x => x.Realm, y => y.MapFrom(s => this.user));

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

            //Character
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

            //User
            this.CreateMap<GuildCommUser, GuildCommUserDetailsViewModel>();

            //Realm
            this.CreateMap<Realm, RealmViewModel>()
                .ForMember(x => x.Type, y => y.MapFrom(s => s.RealmType.ToString()));

            //Member
            this.CreateMap<Member, MemberViewModel>()
                .ForMember(x => x.MemberSince, y => y.MapFrom(s => s.MemberSince.Date.ToString("dd/MM/yyyy")));

            //Applications
            this.CreateMap<ApplicationCreateInputModel, Application>()
                .ForMember(x => x.Role, y => y.MapFrom(s => (Role)(Enum.Parse(typeof(Role), s.Role))));

            this.CreateMap<Application, ApplicationDetailsViewModel>()
                .ForMember(x => x.Role, y => y.MapFrom(s => s.Role.ToString()));

            this.CreateMap<Application, ApplicationsAllViewModel>()
                .ForMember(x => x.Role, y => y.MapFrom(s => s.Role.ToString()));
        }
    }


}
