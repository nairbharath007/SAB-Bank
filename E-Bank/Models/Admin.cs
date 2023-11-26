using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Bank.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

       // public string AdminPassword { get; set; } = string.Empty;


        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}