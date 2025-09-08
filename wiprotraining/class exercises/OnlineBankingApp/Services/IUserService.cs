public interface IUserService
{
    User Authenticate(string username, string password);
    User GetUserById(int id);
}
