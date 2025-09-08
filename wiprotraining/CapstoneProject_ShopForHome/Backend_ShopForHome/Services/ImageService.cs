using Microsoft.Extensions.Logging;
using ShopForHome.API.DTOs;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<ImageService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public ImageService(IWebHostEnvironment environment, ILogger<ImageService> logger, IHttpClientFactory httpClientFactory)
        {
            _environment = environment;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ImageUploadResponse> UploadImageAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return new ImageUploadResponse { Success = false, Message = "Please select a file to upload" };

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp", ".svg" };
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                    return new ImageUploadResponse { Success = false, Message = "Invalid file type. Please upload a valid image file." };

                if (file.Length > 10 * 1024 * 1024)
                    return new ImageUploadResponse { Success = false, Message = "File size cannot exceed 10MB" };

                var fileName = $"{Guid.NewGuid()}{extension}";
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "products");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                _logger.LogInformation("Image uploaded successfully: {FileName}", fileName);

                return new ImageUploadResponse
                {
                    Success = true,
                    Message = "Image uploaded successfully",
                    ImageUrl = $"/images/products/{fileName}",
                    FileName = fileName
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading image");
                return new ImageUploadResponse
                {
                    Success = false,
                    Message = "Error uploading image: " + ex.Message
                };
            }
        }

        public async Task<ImageUploadResponse> ValidateImageUrlAsync(ValidateImageUrlRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ImageUrl))
                    return new ImageUploadResponse { Success = false, Message = "Please provide an image URL" };

                if (!Uri.TryCreate(request.ImageUrl, UriKind.Absolute, out var uri))
                    return new ImageUploadResponse { Success = false, Message = "Invalid URL format" };

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp", ".svg" };
                var extension = Path.GetExtension(uri.AbsolutePath).ToLower();

                if (!allowedExtensions.Contains(extension))
                    return new ImageUploadResponse { Success = false, Message = "Unsupported image format" };

                if (request.VerifyExists)
                {
                    try
                    {
                        using var httpClient = _httpClientFactory.CreateClient();
                        var response = await httpClient.GetAsync(request.ImageUrl, HttpCompletionOption.ResponseHeadersRead);

                        if (!response.IsSuccessStatusCode)
                            return new ImageUploadResponse { Success = false, Message = "Image URL is not accessible" };

                        var contentType = response.Content.Headers.ContentType?.MediaType;
                        if (contentType == null || !contentType.StartsWith("image/"))
                            return new ImageUploadResponse { Success = false, Message = "URL does not point to a valid image" };
                    }
                    catch (HttpRequestException ex)
                    {
                        return new ImageUploadResponse { Success = false, Message = "Cannot access image URL: " + ex.Message };
                    }
                }

                return new ImageUploadResponse
                {
                    Success = true,
                    Message = "Image URL is valid",
                    ImageUrl = request.ImageUrl
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating image URL: {Url}", request.ImageUrl);
                return new ImageUploadResponse { Success = false, Message = "Error validating URL: " + ex.Message };
            }
        }

        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(imageUrl) || !imageUrl.StartsWith("/images/products/"))
                    return false;

                var fileName = Path.GetFileName(imageUrl);
                var filePath = Path.Combine(_environment.WebRootPath, "images", "products", fileName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    _logger.LogInformation("Image deleted: {FileName}", fileName);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting image: {Url}", imageUrl);
                return false;
            }
        }
    }
}
