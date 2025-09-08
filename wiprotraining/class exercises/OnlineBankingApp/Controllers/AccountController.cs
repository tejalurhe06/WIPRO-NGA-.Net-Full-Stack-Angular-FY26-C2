using Microsoft.AspNetCore.Mvc;

[ServiceFilter(typeof(AuthFilter))]
public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

[ServiceFilter(typeof(AuthFilter))] // AuthFilter can still be registered in DI
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { "Admin" })]
public IActionResult AdminDashboard()
{
    return View();
}


    public IActionResult Dashboard()
    {
        return View();
    }
}
