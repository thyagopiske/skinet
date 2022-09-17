using System.Net;

namespace Api.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base((int) HttpStatusCode.BadRequest)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
