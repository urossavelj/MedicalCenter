using MedicalCenter.Interfaces;
using MedicalCenter.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCenter.Controllers
{
    [ApiController]
    [Authorize(Policy = "Bearer")]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly ILogger<DoctorController> _logger;
        private readonly IDoctorService _doctorService;

        public DoctorController(ILogger<DoctorController> logger, IDoctorService doctorService)
        {
            _logger = logger;
            _doctorService = doctorService;
        }

        /// <summary>
        /// Lists all doctors
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDoctors")]
        public IActionResult GetDoctors()
        {
            var doctors = _doctorService.GetDoctors();
            return Ok(doctors);
        }

        /// <summary>
        /// Creates a new doctor
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = "Doctor")]
        [HttpPost("CreateDoctor")]
        public IActionResult CreateDoctor(CreateDoctorRequest request)
        {
            var doctorCreated = _doctorService.CreateDoctor(request);

            if (doctorCreated)
                return Ok();
            else
                return BadRequest();
        }
    }
}
