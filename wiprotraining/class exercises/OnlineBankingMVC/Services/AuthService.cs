using Microsoft.AspNetCore.Http;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContext;

    public AuthService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public bool IsLoggedIn()
    {
        return _httpContext.HttpContext.Session.GetString("UserId") != null;
    }

    public bool HasRole(string role)
    {
        var userRole = _httpContext.HttpContext.Session.GetString("UserRole");
        return userRole == role;
    }

    public string GetCurrentUserId()
    {
        return _httpContext.HttpContext.Session.GetString("UserId");
    }
}
