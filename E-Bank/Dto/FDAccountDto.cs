using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Dto
{
    public class FDAccountDto
    {
        public int FDAccountId { get; set; }

        
        public int AccountId { get; set; }

        public double Amount { get; set; }

        public int Duration { get; set; }
        public double ROI { get; set; }

        public bool IsActive { get; set; }
        public DateTime MaturityDate { get; set; }
        public double MaturityAmount { get; set; }
        public double InterestReturns { get; set; }
    }
}
