using MedicalCenter.Db.Models;
using MedicalCenter.Requests;

namespace MedicalCenter.Interfaces
{
    public interface IDoctorService
    {
        public List<Doctor> GetDoctors();
        public bool CreateDoctor(CreateDoctorRequest request);

    }
}
