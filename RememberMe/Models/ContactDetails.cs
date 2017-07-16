using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RememberMe.Models
{
    public class ContactDetails
    {
        [Column("Email")]
        [StringLength(255)]
        public string Email {get; set;}
        [Column("Phone")]
        [StringLength(255)]
        public string Phone{get; set;}
    }
}