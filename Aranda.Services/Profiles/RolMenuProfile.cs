using Aranda.Models;
using Aranda.Services.ModelView;
using AutoMapper;

namespace Aranda.Services.Profiles
{
    public class RolMenuProfile : Profile
    {
        public RolMenuProfile()
        {
            CreateMap<RolMenu, RolMenuModelView>().
               ForMember(x => x.RolMenuId, y => y.MapFrom(src => src.RolMenuId)).
               ForMember(x => x.RolId, y => y.MapFrom(src => src.RolId)).
               ForMember(x => x.MenuId, y => y.MapFrom(src => src.MenuId)).
               ReverseMap();
        }
    }
}