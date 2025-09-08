using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHome.API.DTOs;
using ShopForHome.API.Services;
using System.Text;
using CsvHelper;
using System.Globalization;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class AdminProductsController : ControllerBase
    {
        private readonly IAdminProductService _productService;
        private readonly ImageService _imageService;
        private readonly ILogger<AdminProductsController> _logger;

        public AdminProductsController(IAdminProductService productService, ImageService imageService,
            ILogger<AdminProductsController> logger)
        {
            _productService = productService;
            _imageService = imageService;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStockProducts()
        {
            var products = await _productService.GetLowStockProductsAsync();
            return Ok(products);
        }

        [HttpPost("bulk-upload")]
        public async Task<IActionResult> BulkUploadProducts(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            if (Path.GetExtension(file.FileName).ToLower() != ".csv")
                return BadRequest("Only CSV files are allowed");

            try
            {
                var products = new List<CreateProductDTO>();
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<dynamic>().ToList();
                    foreach (var record in records)
                    {
                        products.Add(new CreateProductDTO
                        {
                            Name = record.Name,
                            Description = record.Description,
                            Price = decimal.Parse(record.Price),
                            StockQuantity = int.Parse(record.StockQuantity),
                            ImageUrl = record.ImageUrl,
                            CategoryId = int.Parse(record.CategoryId)
                        });
                    }
                }

                var result = await _productService.BulkUploadProductsAsync(products);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing CSV file: {ex.Message}");
            }
        }

        [HttpGet("bulk-template")]
        public IActionResult DownloadBulkTemplate()
        {
            var instructions = "# IMAGE URL INSTRUCTIONS:\n" +
                              "# Option 1: Upload images via /api/admin/products/upload-image\n" +
                              "# Option 2: Use any public image URL from the web\n" +
                              "# Option 3: Use relative paths after uploading\n\n";

            var template = instructions +
                          "Name,Description,Price,StockQuantity,ImageUrl,CategoryId\n" +
                          "Example Product 1,Description 1,99.99,10,https://example.com/image1.jpg,1\n" +
                          "Example Product 2,Description 2,149.99,5,/images/products/abc123.jpg,2\n" +
                          "Example Product 3,Description 3,199.99,15,http://yourdomain.com/images/xyz456.png,1";

            return File(Encoding.UTF8.GetBytes(template), "text/csv", "product_bulk_template.csv");
        }

        [HttpPost("{id}/update-stock")]
        public async Task<IActionResult> UpdateProductStock(int id, [FromBody] int newStockQuantity)
        {
            try
            {
                await _productService.UpdateStockAsync(id, newStockQuantity);
                return Ok(new { message = "Stock updated successfully", newStockQuantity });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{id}/toggle-status")]
        public async Task<IActionResult> ToggleProductStatus(int id)
        {
            try
            {
                await _productService.ToggleProductStatusAsync(id);
                return Ok(new { message = "Product status toggled successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetProductStats()
        {
            var stats = await _productService.GetProductStatsAsync();
            return Ok(stats);
        }

        [HttpPost("update-prices")]
        public async Task<IActionResult> UpdateProductPrices([FromBody] PriceUpdateRequest request)
        {
            try
            {
                await _productService.UpdateProductPricesAsync(request.PercentageChange);
                return Ok(new { message = $"Prices updated by {request.PercentageChange}%" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var result = await _imageService.UploadImageAsync(file);

            if (result.Success)
            {
                var fullImageUrl = $"{Request.Scheme}://{Request.Host}{result.ImageUrl}";
                return Ok(new
                {
                    success = true,
                    message = result.Message,
                    imageUrl = fullImageUrl,
                    relativeUrl = result.ImageUrl,
                    fileName = result.FileName
                });
            }

            return BadRequest(new { success = false, message = result.Message });
        }

        [HttpPost("validate-image-url")]
        public async Task<IActionResult> ValidateImageUrl([FromBody] ValidateImageUrlRequest request)
        {
            var result = await _imageService.ValidateImageUrlAsync(request);

            if (result.Success)
                return Ok(new { success = true, message = result.Message, imageUrl = result.ImageUrl });

            return Ok(new { success = false, message = result.Message });
        }

        [HttpDelete("delete-image")]
        public async Task<IActionResult> DeleteImage([FromBody] string imageUrl)
        {
            var success = await _imageService.DeleteImageAsync(imageUrl);

            if (success)
                return Ok(new { success = true, message = "Image deleted successfully" });

            return BadRequest(new { success = false, message = "Failed to delete image" });
        }
    }
}
