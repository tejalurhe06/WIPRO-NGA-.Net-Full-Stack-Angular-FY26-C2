using Microsoft.EntityFrameworkCore;
using Moq;
using ShopForHome.API.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace ShopForHome.API.Tests.Utilities
{
    public static class TestUtilities
    {
        public static ShopForHomeDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ShopForHomeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name for each test
                .Options;

            return new ShopForHomeDbContext(options);
        }

        public static Mock<HttpContext> GetMockHttpContextWithUser(int userId = 1, string role = "User")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(c => c.User).Returns(claimsPrincipal);

            return mockHttpContext;
        }

        public static IHttpContextAccessor GetMockHttpContextAccessor(int userId = 1, string role = "User")
        {
            var mockHttpContext = GetMockHttpContextWithUser(userId, role);
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(a => a.HttpContext).Returns(mockHttpContext.Object);

            return mockHttpContextAccessor.Object;
        }
    }
}