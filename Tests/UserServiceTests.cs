using Microsoft.Extensions.Logging;
using Moq;
using MedicalCenter.Db;
using MedicalCenter.Requests;
using MedicalCenter.Services;

namespace MedicalCenter.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<ILogger<UserService>> _mockLogger;

        [TestInitialize]
        public void Setup()
        {
            // Create a mock logger
            _mockLogger = new Mock<ILogger<UserService>>();
        }

        [TestMethod]
        public void GetUsers_ReturnsListOfUsers()
        {
            // Arrange
            var userService = new UserService(_mockLogger.Object);

            // Act
            var actualUsers = userService.GetUsers();

            // Assert
            Assert.IsNotNull(actualUsers);
        }

        [TestMethod]
        public void CreateUser_SuccessfulCreation_ReturnsTrue()
        {
            // Arrange
            var userService = new UserService(_mockLogger.Object); // Instantiate your UserService
            var request = new CreateUserRequest
            {
                Username = "newuser",
                Password = "newpass",
                UserType = Enums.UserType.Patient
            };

            // Mock the Context and SaveChanges
            var mockContext = new Mock<Context>();
            mockContext.Setup(c => c.SaveChanges()).Returns(1);

            // Act
            var result = userService.CreateUser(request);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
