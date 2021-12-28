using System.ComponentModel.DataAnnotations;

namespace Aranda.Services.ModelView
{
    public class RolMenuModelView
    {
        [Required]
        public int RolMenuId { get; set; }

        [Required]
        public int RolId { get; set; }

        [Required]
        public int MenuId { get; set; }
    }
}