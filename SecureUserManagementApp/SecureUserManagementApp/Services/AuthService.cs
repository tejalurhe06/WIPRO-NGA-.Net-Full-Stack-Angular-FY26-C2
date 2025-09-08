using SecureUserManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureUserManagementApp.Services
{
    public class AuthService
    {
        private List<User> _users = new List<User>();


        public bool Register(string username, string password)
        {
            if (_users.Any(u => u.Username == username))
                return false;

            var user = new User
            {
                Username = username,
                HashedPassword = User.HashPassword(password)
            };

            _users.Add(user);
            Logger.LogInfo($"User {username} registered.");
            return true;
        }

        public bool Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            if (user == null) return false;

            return user.HashedPassword == User.HashPassword(password);
        }
    }
}
