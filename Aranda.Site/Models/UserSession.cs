using System;

namespace Aranda.Site.Models
{
    [Serializable]
    public class UserSession
    {
        public string FullName { get; set; }

        public string UserName { get; set; }

        public int Id { get; set; }
    }
}