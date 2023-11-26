using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Models
{
    public class Query
    {
        [Key]
        public int QueryId { get; set; }

        public string QueryText { get; set; }

        public DateTime QueryDate { get; set; }

        public string ReplyQuery { get; set; }

        public DateTime ReplyDate { get; set; }

        public bool QueryStatus { get; set; }

       // public bool IsActive { get; set; }


        public Customer Customer { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; } = 0;
    }
}
