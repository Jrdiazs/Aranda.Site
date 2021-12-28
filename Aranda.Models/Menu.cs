
using Dapper;

namespace Aranda.Models
{
    [Table("Menu")]
    public class Menu
    {
        [Key]
        [Column("MenuId")]
        public int MenuId { get; set; }

        [Column("MenuName")]
        public string MenuName { get; set; }

        [Column("MenuUrl")]
        public string MenuUrl { get; set; }

        [Column("MenuParentId")]
        public int? MenuParentId { get; set; }

        [Column("OrderMenu")]
        public int? OrderMenu { get; set; }

        [Column("Active")]

        public bool Active { get; set; }
        
        [Column("MenuAction")]
        public string MenuAction { get; set; }
        
        [Column("MenuController")]
        public string MenuController { get; set; }

        [NotMapped]
        public Menu MenuParent { get; set; } 
    }
}