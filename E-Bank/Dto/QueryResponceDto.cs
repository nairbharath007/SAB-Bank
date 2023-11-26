using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Dto
{
    public class QueryResponceDto
    {
     

        public string ReplyQuery { get; set; }

        public DateTime ReplyDate { get; set; }


        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
    }
}
