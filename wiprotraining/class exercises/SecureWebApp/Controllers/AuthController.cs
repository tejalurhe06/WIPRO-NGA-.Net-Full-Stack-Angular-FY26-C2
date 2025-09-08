using Microsoft.AspNetCore.Mvc;
using SecureWebApp.Models;
using SecureWebApp.Services;

namespace SecureWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var tokenResponse = await _authService.Authenticate(login);

            if (tokenResponse == null)
                return Unauthorized();

            // Set HttpOnly cookie (optional additional security measure)
            Response.Cookies.Append("X-Access-Token", tokenResponse.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Only send over HTTPS
                SameSite = SameSiteMode.Strict
            });

            return Ok(tokenResponse);
        }
    }
}