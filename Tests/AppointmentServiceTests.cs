using MedicalCenter.Requests;
using MedicalCenter.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests
{
    [TestClass]
    public class AppointmentServiceTests
    {
        private Mock<ILogger<AppointmentService>> _mockLogger;

        [TestInitialize]
        public void Setup()
        {
            // Create a mock logger
            _mockLogger = new Mock<ILogger<AppointmentService>>();
        }

        [TestMethod]
        public void GetAppointments_ReturnsListOfAppointments()
        {
            // Arrange
            var appointmentService = new AppointmentService(_mockLogger.Object);

            // Act
            var actualAppointments = appointmentService.GetAppointments();

            // Assert
            Assert.IsNotNull(actualAppointments);
        }

        [TestMethod]
        public void GetDoctorsAppointments_ValidUserId_ReturnsListOfAppointments()
        {
            // Arrange
            var appointmentService = new AppointmentService(_mockLogger.Object); 

            // Act
            var actualAppointments = appointmentService.GetDoctorsAppointments(1);

            // Assert
            Assert.IsNotNull(actualAppointments);
        }

        [TestMethod]
        public void GetBookedAppointmentsForDoctorForTimeFrame_ValidRequest_ReturnsListOfAppointments()
        {
            // Arrange
            var appointmentService = new AppointmentService(_mockLogger.Object); // Instantiate your AppointmentService
            var request = new GetDoctorAppointmentsRequest
            {
                DoctorId = 1,
                From = DateTime.Now,
                To = DateTime.Now.AddHours(2)
            };

            // Act
            var actualAppointments = appointmentService.GetBookedAppointmentsForDoctorForTimeFrame(request);

            // Assert
            Assert.IsNotNull(actualAppointments);
        }

        [TestMethod]
        public void CreateAppointment_SuccessfulCreation_ReturnsTrue()
        {
            // Arrange
            var appointmentService = new AppointmentService(_mockLogger.Object);
            var request = new CreateAppointmentRequest
            {
                DoctorId = 1,
                BookingTime = DateTime.Now,
                PatientId = 1
            };

            // Act
            var result = appointmentService.CreateAppointment(request);

            // Assert
            Assert.IsTrue(result);
        }
    }
}