using Aranda.Services.ModelView;
using System.Collections.Generic;

namespace Aranda.Services.Responses
{
    public class MenuResponse : ResponseData<MenuModelView>
    {
        public MenuResponse()
        {
            Data = new MenuModelView();
        }
    }

    public class MenuResponseList : ResponseData<List<MenuModelView>>
    {
        public MenuResponseList()
        {
            Data = new List<MenuModelView>();
        }
    }

    public class MenuResponseListItems : ResponseData<List<MenuItems>>
    {
        public MenuResponseListItems()
        {
            Data = new List<MenuItems>();
        }
    }
}