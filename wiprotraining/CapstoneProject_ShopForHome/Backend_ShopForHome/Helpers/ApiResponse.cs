namespace ShopForHome.API.Helpers
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public string? ErrorCode { get; set; }
        public string? Exception { get; set; }
        public string? StackTrace { get; set; }

        public ApiResponse(int statusCode, string message = "")
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}