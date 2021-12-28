using Aranda.Services.Responses;
using System.ComponentModel.DataAnnotations;

namespace Aranda.Site.Models
{
    public enum EnumSearchUser
    {
        All = 0,
        FullName = 1,
        ByRol = 2
    }

    public class UserFilterByName
    {
        [MaxLength(300)]
        public string FullName { get; set; }

        public bool? Search { get; set; }
    }

    public class UserFilterByRol
    {
        public int? RolId { get; set; }

        public string Message { get; set; }
    }

    public class UserViewModel
    {
        public UserAppResponseList Data { get; set; } = new UserAppResponseList();

        public bool Edit { get; set; }

        public bool Delete { get; set; }
    }

    public class UserModelRequest : UserAppResponse
    {
        public UserModelRequest() : base()
        {
        }
    }
}