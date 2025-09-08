using Microsoft.AspNetCore.Mvc;
using ShopForHome.API.DTOs;
using ShopForHome.API.Services;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDTO createUserDto)
        {
            try
            {
                var userDto = await _authService.RegisterAsync(createUserDto);
                return Ok(userDto);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var loginResponse = await _authService.LoginAsync(loginRequest);
                return Ok(loginResponse);
            }
            catch (ApplicationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
