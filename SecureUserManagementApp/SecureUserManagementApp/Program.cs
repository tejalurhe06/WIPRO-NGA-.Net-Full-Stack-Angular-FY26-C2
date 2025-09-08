using SecureUserManagementApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureUserManagementApp
{
    class Program
    {
        static void Main()
        {
            var authService = new AuthService();

            try
            {
                Console.WriteLine("Registering user...");
                if (authService.Register("user1", "password123"))
                    Console.WriteLine("User registered successfully.");

                Console.WriteLine("Logging in...");
                if (authService.Login("user1", "password123"))
                    Console.WriteLine("Login successful!");
                else
                    Console.WriteLine("Login failed!");

                string sensitive = "SecretMessage123";
                string encrypted = CryptoService.Encrypt(sensitive);
                string decrypted = CryptoService.Decrypt(encrypted);

                Console.WriteLine($"Encrypted: {encrypted}");
                Console.WriteLine($"Decrypted: {decrypted}");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                Console.WriteLine("Something went wrong.");
            }
        }
    }
}
