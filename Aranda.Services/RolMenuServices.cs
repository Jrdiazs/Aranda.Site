using Aranda.Data;
using Aranda.Models;
using Aranda.Services.ModelView;
using Aranda.Services.Responses;
using Aranda.Tools;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aranda.Services
{
    public class RolMenuServices : BaseServices, IRolMenuServices
    {
        private readonly IRolMenuRepository _rolMenuRepository;
        private readonly IMenuRepository _menuRepository;

        public RolMenuServices(IRolMenuRepository rolMenuRepository, IMenuRepository menuRepository, IMapper mapper): base(mapper)
        {
            _rolMenuRepository = rolMenuRepository ?? throw new ArgumentNullException(nameof(rolMenuRepository));
            _menuRepository = menuRepository ?? throw new ArgumentNullException(nameof(menuRepository));
        }

        public MenuResponseListItems GetMenus(int rolId) 
        {
            var response = new MenuResponseListItems();
            try
            {
                var menusAdd = new List<Menu>();
                var result = new List<MenuItems>();
            
                var rolMenus = _rolMenuRepository.GetMenusFromByRolId(rolId);

                if (!rolMenus.Any()) 
                {
                    response.Error($"El rol {rolId} no tiene menus configurados");
                    return response;
                }

                var menuHome = _menuRepository.GetFindId(1);
                var menus = rolMenus.Select(x => x.Menu).ToList();

                menusAdd.AddRange(menus);
                menusAdd.Add(menuHome);

                foreach (var menu in menus)
                    SearchParents(menu, menusAdd);

                var menuParents = menusAdd.Where(x => !x.MenuParentId.HasValue).OrderBy(y => y.OrderMenu).ToList();

                foreach (var menuParent in menuParents)
                {
                    MenuItems item = new MenuItems()
                    {
                        Menu = Mapper.Map<MenuModelView>(menuParent)
                    };
                    var childrens = SearchChildrens(item, menus);
                    item.MenuChildrens.AddRange(childrens);
                    result.Add(item);
                }

                response.Ok(result);

            }
            catch (Exception ex)
            {
                Logger.ErrorFatal(ex); 
                response.Error(ex);
            }
            return response;
           
        }

        private void SearchParents(Menu menu, List<Menu> menus) 
        {
            if (menu.MenuParentId.HasValue && !menus.Any(x => x.MenuId == menu.MenuParentId))
            {
                var menuBd = _menuRepository.GetFindId(menu.MenuParentId.Value);

                if (menu.MenuParentId.HasValue) 
                {
                    var parent = _menuRepository.GetFindId(menu.MenuParentId.Value);
                    menuBd.MenuParent = parent;
                }

               menus.Add(menuBd);

                if (menuBd.MenuParentId.HasValue && !menus.Any(x => x.MenuId == menuBd.MenuParentId))
                    SearchParents(menuBd, menus);
                else
                    return;

            }
            else
                return;
        }

        /// <summary>
        /// Busca los menus hijos
        /// </summary>
        /// <param name="menu">menu padre</param>
        /// <param name="menus">listado de menus</param>
        private List<MenuItems> SearchChildrens(MenuItems menu, List<Menu> menus)
        {
            try
            {

                var menusChildrens = new List<MenuItems>();
                if (menus.Any(x => x.MenuParentId == menu.Menu.MenuId))
                {
                    var menusSearch = menus.Where(x => x.MenuParentId == menu.Menu.MenuId).OrderBy(x => x.OrderMenu).ToList();
                    foreach (var menuParent in menusSearch)
                    {
                        MenuItems item = new MenuItems()
                        {
                            Menu = Mapper.Map<MenuModelView>(menuParent)
                        };

                        if (menus.Any(x => x.MenuParentId == menuParent.MenuId))
                        {
                            var childrens = SearchChildrens(item, menus);
                            if (childrens.Any())
                                item.MenuChildrens.AddRange(childrens);
                        }

                        menusChildrens.Add(item);
                    }
                }
                return menusChildrens ?? new List<MenuItems>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public interface IRolMenuServices
    {
        MenuResponseListItems GetMenus(int rolId);
    }
}