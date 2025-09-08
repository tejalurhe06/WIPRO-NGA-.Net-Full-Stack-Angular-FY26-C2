using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureUserManagementApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureUserManagementApp.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestUserRegistrationAndLogin()
        {
            var authService = new AuthService();
            Assert.IsTrue(authService.Register("user", "1234"));
            Assert.IsTrue(authService.Login("user", "1234"));
        }

        [TestMethod]
        public void TestEncryptionDecryption()
        {
            string data = "Secret";
            string encrypted = CryptoService.Encrypt(data);
            string decrypted = CryptoService.Decrypt(encrypted);
            Assert.AreEqual(data, decrypted);
        }
    }
}
