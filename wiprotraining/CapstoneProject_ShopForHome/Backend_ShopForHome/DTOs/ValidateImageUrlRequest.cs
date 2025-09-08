namespace ShopForHome.API.DTOs
{
    public class ValidateImageUrlRequest
    {
        public string ImageUrl { get; set; } = string.Empty;
        public bool VerifyExists { get; set; } = false;
    }
}

namespace ShopForHome.API.DTOs
{
    public class ImageUploadResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }
}