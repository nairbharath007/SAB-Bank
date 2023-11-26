using System.Net;

namespace E_Bank.Models
{
    public class ErrorResponce
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public ErrorResponce(string message)
        {
            Message = message;
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }

    }
}
