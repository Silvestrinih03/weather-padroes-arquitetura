using Newtonsoft.Json;
using System.Net;

namespace Application.Middleware
{
    public class ErrorDetails
    {
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }

        public ErrorDetails(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(new { Message });
        }
    }
}