namespace Api.Errors
{
    public class ApiResponse
    {

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Request ruim :(",
                401 => "Parece que vc não tem autorização pra isso :(",
                404 => "Cara, não encontrei não :(",
                500 => "Vish, erro interno do servidor :(",
                _ => null
            };
        }
    }
}
