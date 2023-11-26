using E_Bank.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Dto
{
    public class AdminDto
    {
        public int AdminId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        // public string AdminPassword { get; set; } = string.Empty;
       // public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
