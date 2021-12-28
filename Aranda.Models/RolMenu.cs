
using Dapper;
namespace Aranda.Models
{
    [Table("RolMenu")]
    public class RolMenu
    {
        [Key]
        [Column("RolMenuId")]
        public int RolMenuId { get; set; }

        [Column("RolId")]
        public int RolId { get; set; }

        [Column("MenuId")]
        public int MenuId { get; set; }

        [NotMapped]
        public Rol Rol { get; set; }

        [NotMapped]
        public Menu Menu { get; set; }
    }
}