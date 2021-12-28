using Aranda.Models;
using Aranda.Services.ModelView;
using AutoMapper;

namespace Aranda.Services.Profiles
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, MenuModelView>().
               ForMember(x => x.MenuId, y => y.MapFrom(src => src.MenuId)).
               ForMember(x => x.MenuName, y => y.MapFrom(src => src.MenuName)).
               ForMember(x => x.MenuController, y => y.MapFrom(src => src.MenuController)).
               ForMember(x => x.MenuAction, y => y.MapFrom(src => src.MenuAction)).
               ForMember(x => x.MenuUrl, y => y.MapFrom(src => src.MenuUrl)).
               ForMember(x => x.MenuParentId, y => y.MapFrom(src => src.MenuParentId)).
               ForMember(x => x.OrderMenu, y => y.MapFrom(src => src.OrderMenu)).
               ForMember(x => x.Active, y => y.MapFrom(src => src.Active)).
               ReverseMap();
        }
    }
}