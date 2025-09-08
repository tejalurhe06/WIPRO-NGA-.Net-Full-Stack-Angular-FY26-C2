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
        return _httpContext.HttpContext.Session.GetString("User") != null;
    }
}
