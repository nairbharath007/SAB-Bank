using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Bank.Models
{
    public class FDAccount
    {

        [Key]
        public int FDAccountId { get; set; }

        public Account Account { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; }

        public DateTime OpeningDate { get; set; }

        public double Amount { get; set; }

        public int Duration { get; set; }
        public double ROI { get; set; }

        public bool IsActive { get; set; }

        public DateTime MaturityDate { get; set; }
        public double MaturityAmount { get; set; }
        public double InterestReturns { get; set; }
    }
}
