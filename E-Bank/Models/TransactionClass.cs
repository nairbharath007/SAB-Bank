using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Models
{
    public class TransactionClass
    {
        [Key]
        public int TransactionId { get; set; }

        public string TransactionType { get; set; }

        public double TransactionAmount { get; set; }

        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }

        public string State { get; set; }//pending or approved show

        public bool IsActive { get; set; }//soft delete

        public int ReceiverId { get; set; } = 0;

        public double UpdatedBalance { get; set; }

        public Account Account { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; } = 0;


    }
}
