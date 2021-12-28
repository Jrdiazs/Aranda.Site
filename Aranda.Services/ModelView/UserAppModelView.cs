using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Aranda.Services.ModelView
{
    public class UserAppModelView
    {
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("FirstName")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("LastName")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(500)]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("Phone")]
        public string Phone { get; set; }

        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Birthday")]
        public DateTime? Birthday { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(200)]
        [DisplayName("Password")]
        public string Pw { get; set; }

        [Compare("Pw", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPw { get; set; }

        [Required]
        [DisplayName("Rol Id")]
        public int RolId { get; set; }

        [DisplayName("Full Name")]
        public string FullName
        { get { return $"{FirstName} {LastName}"; } }

        [DisplayName("Rol Name")]
        public string RolName { get; set; }
    }
}