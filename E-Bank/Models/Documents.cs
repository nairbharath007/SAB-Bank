using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Bank.Models
{
    public class Documents
    {
        [Key]
        public int DocumentId { get; set; }
        public byte[] DocumentData { get; set; }

        public string DocumentType { get; set; }

        public DateTime UploadDate { get; set; }

        public string Status { get; set; }




        public Customer Customer { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
    }
}