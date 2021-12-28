using Aranda.Models;
using Aranda.Services.ModelView;
using AutoMapper;

namespace Aranda.Services.Profiles
{
    public class RolProfile : Profile
    {
        public RolProfile()
        {
            CreateMap<Rol, RolModelView>().
               ForMember(x => x.RolId, y => y.MapFrom(src => src.RolId)).
               ForMember(x => x.RolName, y => y.MapFrom(src => src.RolName)).
               ForMember(x => x.Active, y => y.MapFrom(src => src.Active)).
               ReverseMap();
        }
    }
}