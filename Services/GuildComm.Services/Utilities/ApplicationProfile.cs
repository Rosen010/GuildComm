namespace GuildComm.Services.Utilities
{
    using System;
    using AutoMapper;

    using GuildComm.Data.Models;
    using GuildComm.Data.Models.Enums;
    using GuildComm.Web.ViewModels.Applications;

    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            this.CreateMap<ApplicationCreateInputModel, Application>()
                .ForMember(x => x.Role, y => y.MapFrom(s => (Role)(Enum.Parse(typeof(Role), s.Role))));

            this.CreateMap<Application, ApplicationDetailsViewModel>()
                .ForMember(x => x.Role, y => y.MapFrom(s => s.Role.ToString()));

            this.CreateMap<Application, ApplicationsAllViewModel>()
                .ForMember(x => x.Role, y => y.MapFrom(s => s.Role.ToString()));
        }
    }
}
