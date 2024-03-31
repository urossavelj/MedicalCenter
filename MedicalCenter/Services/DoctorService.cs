using MedicalCenter.Db;
using MedicalCenter.Db.Models;
using MedicalCenter.Interfaces;
using MedicalCenter.Requests;

namespace MedicalCenter.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ILogger<DoctorService> _logger;

        public DoctorService(ILogger<DoctorService> logger)
        {
            _logger = logger;
        }

        public List<Doctor> GetDoctors()
        {
            using (var db = new Context())
            {
                var doctors = db.Doctors.ToList();
                return doctors;
            }
        }

        public bool CreateDoctor(CreateDoctorRequest request)
        {
            using (var db = new Context())
            {
                var doctor = new Doctor
                {
                    Name = request.Name,
                    UserId = request.UserId,
                    WorkingTimeFrom = request.WorkingTimeFrom,
                    WorkingTimeTo = request.WorkingTimeTo
                };

                db.Doctors.Add(doctor);
                var response = db.SaveChanges();

                if (response == 1)
                    return true;
                else
                {
                    _logger.LogError("An error occured when creating a doctor");
                    return false;
                }
            }
        }

    }
}
