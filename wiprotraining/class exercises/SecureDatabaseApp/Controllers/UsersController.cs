using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureDatabaseApp.Data;
using SecureDatabaseApp.Models;
using SecureDatabaseApp.Services;

namespace SecureDatabaseApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ApplicationDbContext _context;

    public UsersController(IUserService userService, ApplicationDbContext context)
    {
        _userService = userService;
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(new { user.Id, user.Username, user.Email, user.FullName });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers(string? username)
    {
        IQueryable<User> query = _context.Users;

        if (!string.IsNullOrEmpty(username))
        {
            query = query.Where(u => u.Username == username); 
        }

        var users = await query.Select(u => new { u.Id, u.Username, u.Email }).ToListAsync(); 
        return Ok(users);
    }
}