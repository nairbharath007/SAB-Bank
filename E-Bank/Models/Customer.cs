using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; } = string.Empty;
        public double ContactDetails { get; set; } = 0;
        public string State { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public List<Account> Accounts { get; set; }

       public List<Documents> Documents { get; set; }

       public List<Query> Queries { get; set; }

        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }


    }
}