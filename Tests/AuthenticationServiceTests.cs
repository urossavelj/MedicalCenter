using MedicalCenter.Db.Models;
using MedicalCenter.Requests;
using MedicalCenter.Services;
using Microsoft.Extensions.Configuration;

namespace Tests
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        private IConfiguration _configuration;

        [TestInitialize]
        public void Setup()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json") 
                .Build();
        }

        [TestMethod]
        public void Authenticate_ValidUser_ReturnsAuthenticationToken()
        {
            // Arrange
            var authService = new AuthenticationService(_configuration); 
            var request = new AuthenticateUserRequest
            {
                Username = "Franci",
                Password = "Test"
            };

            // Act
            var result = authService.Authenticate(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<AuthenticationToken>(result);
            Assert.IsFalse(string.IsNullOrEmpty(result.Token));
        }

        [TestMethod]
        public void Authenticate_InvalidUser_ReturnsNull()
        {
            // Arrange
            var authService = new AuthenticationService(_configuration);
            var request = new AuthenticateUserRequest
            {
                Username = "nonexistentuser",
                Password = "invalidpassword"
            };

            // Act
            var result = authService.Authenticate(request);

            // Assert
            Assert.IsNull(result);
        }
    }
}
