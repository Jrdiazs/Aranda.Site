using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Aranda.Services.Request
{
    public class UserAppRequest
    {
        public string FullName { get; set; }

        public int? RolId { get; set; }
    }

    public class UserLogin 
    {
        [Required]
        [DisplayName("User Name")]
        [MaxLength(50)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(50)]
        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }
    }


   
}