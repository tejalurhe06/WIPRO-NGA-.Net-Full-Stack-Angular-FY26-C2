using ShopForHome.API.DTOs;

namespace ShopForHome.API.Interfaces
{
    public interface IImageService
    {
        Task<ImageUploadResponse> UploadImageAsync(IFormFile file);
        Task<ImageUploadResponse> ValidateImageUrlAsync(ValidateImageUrlRequest request);
        Task<bool> DeleteImageAsync(string imageUrl);
    }
}
