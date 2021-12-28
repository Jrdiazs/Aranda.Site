using Aranda.Models;
using Aranda.Services.ModelView;
using AutoMapper;

namespace Aranda.Services.Profiles
{
    public class UserAppProfile : Profile
    {
        public UserAppProfile()
        {
            CreateMap<UserApp, UserAppModelView>().
               ForMember(x => x.Id, y => y.MapFrom(src => src.UserId)).
               ForMember(x => x.UserName, y => y.MapFrom(src => src.UserName)).
               ForMember(x => x.Pw, y => y.MapFrom(src => src.Pw)).
               ForMember(x => x.RolName, y => y.MapFrom(src => src.Role != null ? src.Role.RolName : string.Empty)).
               ForMember(x => x.FirstName, y => y.MapFrom(src => src.FirstName)).
               ForMember(x => x.LastName, y => y.MapFrom(src => src.LastName)).
               ForMember(x => x.Address, y => y.MapFrom(src => src.Address)).
               ForMember(x => x.Phone, y => y.MapFrom(src => src.Phone)).
               ForMember(x => x.Email, y => y.MapFrom(src => src.Email)).
               ForMember(x => x.Birthday, y => y.MapFrom(src => src.Birthday)).
               ForMember(x => x.RolId, y => y.MapFrom(src => src.RolId)).
               ReverseMap();
        }
    }
}