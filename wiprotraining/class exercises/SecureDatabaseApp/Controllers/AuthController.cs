using Microsoft.AspNetCore.Mvc;
using SecureDatabaseApp.Models;
using SecureDatabaseApp.Services;

namespace SecureDatabaseApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegistrationDto userRegistrationDto)
    {
        var user = await _userService.RegisterUserAsync(userRegistrationDto);

        if (user == null)
        {
            return BadRequest("Username or Email already exists.");
        }

        return Ok(new { user.Id, user.Username, user.Email });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        var user = await _userService.AuthenticateUserAsync(userLoginDto);

        if (user == null)
        {
            return Unauthorized("Invalid username or password.");
        }

        return Ok(new { message = "Login successful!", user.Id, user.Username });
    }
}