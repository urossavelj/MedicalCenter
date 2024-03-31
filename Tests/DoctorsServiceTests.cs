using MedicalCenter.Db.Models;
using MedicalCenter.Db;
using MedicalCenter.Requests;
using MedicalCenter.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace MedicalCenter.Tests
{
    [TestClass]
    public class DoctorServiceTests
    {
        private Mock<ILogger<DoctorService>> _mockLogger;

        [TestInitialize]
        public void Setup()
        {
            // Create a mock logger
            _mockLogger = new Mock<ILogger<DoctorService>>();
        }

        [TestMethod]
        public void GetDoctors_ReturnsListOfDoctors()
        {
            // Arrange
            var doctorService = new DoctorService(_mockLogger.Object); 

            // Act
            var actualDoctors = doctorService.GetDoctors();

            // Assert
            Assert.IsNotNull(actualDoctors);
        }

        [TestMethod]
        public void CreateDoctor_SuccessfulCreation_ReturnsTrue()
        {
            // Arrange
            var doctorService = new DoctorService(_mockLogger.Object);
            var request = new CreateDoctorRequest
            {
                Name = "Dr. Brown",
                UserId = 1,
                WorkingTimeFrom = DateTime.Now,
                WorkingTimeTo = DateTime.Now.AddMinutes(30)
            };

            // Act
            var result = doctorService.CreateDoctor(request);

            // Assert
            Assert.IsTrue(result);
        }
    }
}