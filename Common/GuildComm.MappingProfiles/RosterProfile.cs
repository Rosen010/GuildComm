using AutoMapper;
using BNetAPI.Core.Enums;
using BNetAPI.Guilds.Models.RequestModels;
using BNetAPI.Guilds.Models.ResponseModels.ResponseComponents.Roster;
using GuildComm.Common.Constants;
using GuildComm.Web.Models.Items;
using GuildComm.Web.Models.Search;

namespace GuildComm.MappingProfiles
{
    public class RosterProfile : Profile
    {
        public RosterProfile()
        {
            this.CreateMap<SearchViewModel, RosterRequestModel>()
                .ForMember(dest => dest.GuildName, opt => opt.MapFrom(src => src.GuildName.Replace(' ', '-').ToLower()))
                .ForMember(dest => dest.Realm, opt => opt.MapFrom(src => src.Realm.Replace(' ', '-').ToLower()))
                .ForMember(dest => dest.Locale, opt => opt.MapFrom(src => Localizations.MappedLocalizations[src.Namespace]))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => Localizations.MappedRegions[src.Namespace]));

            this.CreateMap<MemberComponent, MemberItem>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Character.Name))
                .ForMember(dest => dest.Rank, opt => opt.MapFrom(src => src.Rank))
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => ((ClassId)src.Character.Class.Id).ToString()));
        }
    }
}
