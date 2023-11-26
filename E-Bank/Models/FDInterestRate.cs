using System.ComponentModel.DataAnnotations;

namespace E_Bank.Models
{
    public class FDInterestRate
    {

        [Key]
        public int FDInterestRateId { get; set; }
        public int MinDuration { get; set; }
        public int MaxDuration { get; set; } = 0;
        public double ROI { get; set; }
    }
}
