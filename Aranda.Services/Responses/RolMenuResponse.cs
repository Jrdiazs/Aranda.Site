using Aranda.Services.ModelView;
using System.Collections.Generic;

namespace Aranda.Services.Responses
{
    public class RolMenuResponse : ResponseData<RolMenuModelView>
    {
        public RolMenuResponse()
        {
            Data = new RolMenuModelView();
        }
    }

    public class RolMenuResponseList : ResponseData<List<RolMenuModelView>>
    {
        public RolMenuResponseList()
        {
            Data = new List<RolMenuModelView>();
        }
    }
}