using MedicalCenter.Db.Models;
using MedicalCenter.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MedicalCenter.Requests;
using MedicalCenter.Interfaces;
using MedicalCenter.Services;

namespace MedicalCenter.Controllers
{
    [ApiController]
    [Authorize(Policy = "Bearer")]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private readonly IPatientService _patientService;

        public PatientController(ILogger<PatientController> logger, IPatientService patientService)
        {
            _logger = logger;
            _patientService = patientService;
        }

        /// <summary>
        /// Lists all patients
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPatients")]
        public IActionResult GetPatients()
        {
            var patients = _patientService.GetPatients();
            return Ok(patients);
        }

        /// <summary>
        /// Creates a new patient
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CreatePatient")]
        public IActionResult CreatePatient(CreatePatientRequest request)
        {
            var patientCreated = _patientService.CreatePatient(request);

            if (patientCreated)
                return Ok();
            else
                return BadRequest();
        }
    }
}
