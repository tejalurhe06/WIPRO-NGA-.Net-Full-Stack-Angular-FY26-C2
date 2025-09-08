using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requires authentication for all endpoints
    public class SecureDataController : ControllerBase
    {
        [HttpGet("user-data")]
        [Authorize(Roles = "User,Admin")] // Both User and Admin roles can access
        public IActionResult GetUserData()
        {
            return Ok(new { Message = "This is secure user data", User = User.Identity.Name });
        }

        [HttpGet("admin-data")]
        [Authorize(Roles = "Admin")] // Only Admin role can access
        public IActionResult GetAdminData()
        {
            return Ok(new { Message = "This is secure admin data", User = User.Identity.Name });
        }

        [HttpGet("public-data")]
        [AllowAnonymous] // This endpoint doesn't require authentication
        public IActionResult GetPublicData()
        {
            return Ok(new { Message = "This is public data" });
        }
    }
}