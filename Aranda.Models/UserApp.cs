using Dapper;
using System;

namespace Aranda.Models
{
    [Table("UserApp")]
    public class UserApp
    {
        [Key]
        [Column("UserId")]
        public int UserId { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }

        [Column("Pw")]
        public string Pw { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Birthday")]
        public DateTime? Birthday { get; set; }

        [Column("RolId")]
        public int RolId { get; set; }

        [NotMapped]
        public Rol Role { get; set; }
    }
}