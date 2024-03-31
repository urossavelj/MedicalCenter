using MedicalCenter.Db;
using MedicalCenter.Requests;
using MedicalCenter.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests
{
    [TestClass]
    public class PatientServiceTests
    {
        private Mock<ILogger<PatientService>> _mockLogger;

        [TestInitialize]
        public void Setup()
        {
            // Create a mock logger
            _mockLogger = new Mock<ILogger<PatientService>>();
        }

        [TestMethod]
        public void GetPatients_ReturnsListOfPatients()
        {
            // Arrange
            var patientService = new PatientService(_mockLogger.Object); // Instantiate your PatientService

            // Act
            var actualPatients = patientService.GetPatients();

            // Assert
            Assert.IsNotNull(actualPatients);
        }

        [TestMethod]
        public void CreatePatient_SuccessfulCreation_ReturnsTrue()
        {
            // Arrange
            var patientService = new PatientService(_mockLogger.Object); // Instantiate your PatientService
            var request = new CreatePatientRequest
            {
                Name = "NewPatient",
                UserId = 1
            };

            // Mock the Context and SaveChanges
            var mockContext = new Mock<Context>();
            mockContext.Setup(c => c.SaveChanges()).Returns(1);

            // Act
            var result = patientService.CreatePatient(request);

            // Assert
            Assert.IsTrue(result);
        }
    }
}