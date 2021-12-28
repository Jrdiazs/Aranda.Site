using Dapper;

namespace Aranda.Models
{
    [Table("Rol")]
    public class Rol
    {
        [Key]
        [Column("RolId")]
        public int RolId { get; set; }

        [Column("RolName")]
        public string RolName { get; set; }

        [Column("Active")]
        public bool Active { get; set; }
    }
}