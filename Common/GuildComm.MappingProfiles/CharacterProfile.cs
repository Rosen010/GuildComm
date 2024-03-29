﻿using AutoMapper;
using BNetAPI.Core.Components.Characters.Models.RequestModels;
using BNetAPI.Core.Components.Characters.Models.ResponseModels;
using GuildComm.Common.Constants;
using GuildComm.Web.Models.Character;

namespace GuildComm.MappingProfiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            this.CreateMap<CharacterInputModel, CharacterRequestModel>()
                .ForMember(dest => dest.CharacterName, opt => opt.MapFrom(src => src.CharacterName.ToLower()))
                .ForMember(dest => dest.Realm, opt => opt.MapFrom(src => src.Realm.ToLower()))
                .ForMember(dest => dest.Locale, opt => opt.MapFrom(src => Localizations.MappedLocalizations[src.NameSpace]))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => Localizations.MappedRegions[src.NameSpace]));

            this.CreateMap<CharacterResponse, CharacterViewModel>()
                .ForMember(dest => dest.Faction, opt => opt.MapFrom(src => src.Faction.Name))
                .ForMember(dest => dest.Race, opt => opt.MapFrom(src => src.Race.Name))
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class.Name))
                .ForMember(dest => dest.Spec, opt => opt.MapFrom(src => src.Spec.Name))
                .ForMember(dest => dest.Realm, opt => opt.MapFrom(src => src.Realm.Name))
                .ForMember(dest => dest.Guild, opt => opt.MapFrom(src => src.Guild.Name));
        }
    }
}
