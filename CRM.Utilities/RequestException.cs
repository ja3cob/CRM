using System.Net;

namespace CRM.Utilities
{
    public class RequestException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public RequestException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}