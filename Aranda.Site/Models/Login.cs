using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Aranda.Site.Models
{
    public class Login
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

        public string Message { get; set; }
    }
}