using E_Bank.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Dto
{
    public class QueryDto
    {
        public int QueryId { get; set; }

        public string QueryText { get; set; }

        public DateTime? QueryDate{ get; set; }

      
        [ForeignKey("Customer")]
        public int CustomerId { get; set; } 
    }
}
