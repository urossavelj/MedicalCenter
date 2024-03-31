using MedicalCenter.Db.Models;
using MedicalCenter.Requests;

namespace MedicalCenter.Interfaces
{
    public interface IPatientService
    {
        public List<Patient> GetPatients();
        public bool CreatePatient(CreatePatientRequest request);
    }
}
