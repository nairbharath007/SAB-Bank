using System.Net;

namespace E_Bank.Exceptions
{
    public class UserNotFoundException:Exception
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public UserNotFoundException(string message):base(message)
        {
            this.Message = message;
            StatusCode = (int)HttpStatusCode.NotFound;

        }
    }
}
