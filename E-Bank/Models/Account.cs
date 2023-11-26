using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Models
{
    public class Account
    {
        [Key]
        public int AccountNumber { get; set; }

        public string AccountType { get; set; }
       // public string AccountNumber { get; set; }

        //OpeningDate
        public DateTime OpenningDate { get; set; }
        public double AccountBalance { get; set; }

        //Interest
        public double IntrestRate { get; set; } 

        public bool IsActive { get; set; }

        public Customer Customer { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; } = 0;

        public List<FDAccount> FDAccounts { get; set; }
        public List<TransactionClass> Transactions { get; set; }







    }
}
