using Aranda.Services.ModelView;
using System.Collections.Generic;

namespace Aranda.Services.Responses
{
    public class RolResponse : ResponseData<RolModelView>
    {
        public RolResponse()
        {
            Data = new RolModelView();
        }
    }

    public class RolResponseList : ResponseData<List<RolModelView>>
    {
        public RolResponseList()
        {
            Data = new List<RolModelView>();
        }
    }
}