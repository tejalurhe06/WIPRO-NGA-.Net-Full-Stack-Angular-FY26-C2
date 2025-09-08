namespace ShopForHome.API.Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode { get; }
        public string? ErrorCode { get; }

        public AppException(string message, int statusCode = 500, string? errorCode = null) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}