using System.Net;

namespace EndPoint.Site.Models.IDPayModels
{
    public class IDPayException : Exception
    {
        public HttpStatusCode? HttpStatusCode { get; private set; }

        public IDPayException()
        {
        }
        public IDPayException(HttpStatusCode? httpStatusCode, string? message) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }
        public IDPayException(HttpStatusCode? httpStatusCode, string? message, Exception? innerException) : base(message,
            innerException)
        {
            HttpStatusCode = httpStatusCode;
        }

        public IDPayException(string? message) : this(null, message)
        {
        }
        public IDPayException(string? message, Exception? innerException) : this(null, message, innerException)
        {
        }
    }
}
