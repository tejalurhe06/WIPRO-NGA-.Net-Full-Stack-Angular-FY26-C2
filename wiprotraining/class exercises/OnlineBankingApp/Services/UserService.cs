public class UserService : IUserService
{
    private List<User> _users = new()
    {
        new User { Id = 1, Username = "admin", Password = "admin123", Role = "Admin" },
        new User { Id = 2, Username = "user", Password = "user123", Role = "Customer" }
    };

    public User Authenticate(string username, string password)
    {
        return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
    }

    public User GetUserById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }
}
