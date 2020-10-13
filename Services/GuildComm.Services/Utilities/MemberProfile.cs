namespace GuildComm.Services.Utilities
{
    using AutoMapper;
    using GuildComm.Data.Models;
    using GuildComm.Common.Constants;
    using GuildComm.Web.ViewModels.Members;

    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            this.CreateMap<Member, MemberViewModel>()
                .ForMember(x => x.MemberSince, y => y.MapFrom(s => s.MemberSince.Date.ToString(DateFormats.StandardDateTimeFormat)));
        }
    }
}
