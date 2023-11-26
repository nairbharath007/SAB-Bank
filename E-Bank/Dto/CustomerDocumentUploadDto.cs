namespace E_Bank.Dto
{
    public class CustomerDocumentUploadDto
    {
        public string DocumentType { get; set; }
        public IFormFile DocumentFile { get; set; }
        public int CustomerId { get; set; }

    }
}
