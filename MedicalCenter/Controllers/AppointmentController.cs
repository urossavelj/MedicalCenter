using Microsoft.AspNetCore.Mvc;
using MedicalCenter.Requests;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using MedicalCenter.Interfaces;

namespace MedicalCenter.Controllers
{
    [ApiController]
    [Authorize(Policy = "Bearer")]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(ILogger<AppointmentController> logger, IAppointmentService appointmentService)
        {
            _logger = logger;
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Lists appointments
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAppointments")]
        public IActionResult GetAppointments()
        {
            var appointments = _appointmentService.GetAppointments();
            return Ok(appointments);
        }

        /// <summary>
        /// Lists specific doctor appointments
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Doctor")]
        [HttpGet("GetDoctorsAppointments")]
        public IActionResult GetDoctorsAppointments()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var appointments = _appointmentService.GetDoctorsAppointments(int.Parse(userId));
            return Ok(appointments);
        }

        /// <summary>
        /// Lists specific doctors appointments in a time span
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("GetBookedAppointmentsForDoctorForTimeFrame")]
        public IActionResult GetBookedAppointmentsForDoctorForTimeFrame(GetDoctorAppointmentsRequest request)
        {
            var appointments = _appointmentService.GetBookedAppointmentsForDoctorForTimeFrame(request);
            return Ok(appointments);
        }

        /// <summary>
        /// Creates an appointment
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CreateAppointment")]
        public IActionResult CreateAppointment(CreateAppointmentRequest request)
        {
            var appointmentCreated = _appointmentService.CreateAppointment(request);

            if (appointmentCreated)
                return Ok();
            else
                return BadRequest();
        }

        /// <summary>
        /// Deletes an appointment
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteAppointment/{appointmentId}")]
        public IActionResult DeleteAppointment(int appointmentId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var appointmentDeleted = _appointmentService.DeleteAppointment(appointmentId, int.Parse(userId));

            if (appointmentDeleted)
                return Ok();
            else
                return BadRequest();
        }

        /// <summary>
        /// Updates an appointment time
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = "Doctor")]
        [HttpPost("UpdateAppointmentTime")]
        public IActionResult UpdateAppointmentTime(UpdateAppointmentRequest request)
        {
            var appointmentUpdated = _appointmentService.UpdateAppointmentTime(request);

            if (appointmentUpdated)
                return Ok();
            else
                return BadRequest();
        }
    }
}