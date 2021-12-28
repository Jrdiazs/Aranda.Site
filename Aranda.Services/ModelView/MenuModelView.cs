using Aranda.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aranda.Services.ModelView
{
    public class MenuModelView
    {
        [Required]
        public int MenuId { get; set; }

        [Required]
        [MaxLength(50)]
        public string MenuName { get; set; }

        [Required]
        [MaxLength(100)]
        public string MenuUrl { get; set; }

        public int? MenuParentId { get; set; }

        public int? OrderMenu { get; set; }

        [Required]
        public bool Active { get; set; }


        [MaxLength(100)]
        public string MenuAction { get; set; }

        [Required]
        [MaxLength(100)]
        public string MenuController { get; set; }
    }

    public class MenuItems 
    {
        public MenuModelView Menu { get; set; }

        public List<MenuItems> MenuChildrens { get; set; } = new List<MenuItems>();
    }
}