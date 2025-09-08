using OnlineBankingMVC.Models;

public interface IAuthService
{
    bool IsLoggedIn();
    bool HasRole(string role);
    string GetCurrentUserId();
}
