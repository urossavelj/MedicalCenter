using MedicalCenter.Db;
using MedicalCenter.Db.Models;
using MedicalCenter.Interfaces;
using MedicalCenter.Requests;

namespace MedicalCenter.Services
{
    public class PatientService : IPatientService
    {
        private readonly ILogger<PatientService> _logger;

        public PatientService(ILogger<PatientService> logger)
        {
            _logger = logger;
        }

        public List<Patient> GetPatients()
        {
            using (var db = new Context())
            {
                var patients = db.Patients.ToList();
                return patients;
            }
        }

        public bool CreatePatient(CreatePatientRequest request)
        {
            using (var db = new Context())
            {
                var patient = new Patient
                {
                    Name = request.Name,
                    UserId = request.UserId,
                };

                db.Patients.Add(patient);
                var response = db.SaveChanges();

                if (response == 1)
                    return true;
                else
                {
                    _logger.LogError("An error occured when creating a patient");
                    return false;
                }
            }
        }

    }
}
