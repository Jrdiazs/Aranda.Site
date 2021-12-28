using System.ComponentModel.DataAnnotations;

namespace Aranda.Services.ModelView
{
    public class RolModelView
    {
        [Required]
        public int RolId { get; set; }

        [Required]
        [MaxLength(50)]
        public string RolName { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}