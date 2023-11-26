using E_Bank.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Dto
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }

        public string TransactionType { get; set; }

        public double TransactionAmount { get; set; }

        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }
        public double UpdatedBalance { get; set; }

       //  public string State { get; set; }//pending or approved show

        //  public bool IsActive { get; set; }//soft delete


        // public Account Account { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; } = 0;

        public int ReceiverId { get; set; }= 0;
    }
}
