using E_Bank.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Dto
{
    public class AccountDto
    {
        public int AccountNumber { get; set; }

        public string AccountType { get; set; }
        public double AccountBalance { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; } = 0;


           public DateTime OpenningDate { get; set; }


           public double IntrestRate { get; set; }

           public bool IsActive { get; set; }

          public int TransactionsCount { get; set; } = 0;
    }
}
