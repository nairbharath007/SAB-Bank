namespace E_Bank.Dto
{
    public class TransferDto
    {
        public int TargetAccountNumber { get; set; }

        public int AccountNumber { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }
    }
}
