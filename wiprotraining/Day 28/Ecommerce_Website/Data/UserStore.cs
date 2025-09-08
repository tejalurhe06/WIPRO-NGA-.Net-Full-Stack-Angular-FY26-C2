using Ecommerce_Website.Models;
using System.Collections.Generic;

namespace Ecommerce_Website.Data
{
    public static class UserStore
    {
        public static List<User> Users = new List<User>
        {
            new User{ Username = "admin", Password = "123" },
            new User{ Username = "Tejal", Password = "pass123" }
        };

        // Method to validate login
        public static bool ValidateUser(string username, string password)
        {
            return Users.Exists(u => u.Username == username && u.Password == password);
        }
    }
}
