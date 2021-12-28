using Aranda.Services.ModelView;
using System.Collections.Generic;

namespace Aranda.Services.Responses
{
    public class UserAppResponse : ResponseData<UserAppModelView>
    {
        public UserAppResponse()
        {
            Data = new UserAppModelView();
        }
    }

    public class UserAppResponseList : ResponseData<List<UserAppModelView>>
    {
        public UserAppResponseList()
        {
            Data = new List<UserAppModelView>();
        }
    }
}