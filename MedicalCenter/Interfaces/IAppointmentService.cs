using MedicalCenter.Db.Models;
using MedicalCenter.Requests;

namespace MedicalCenter.Interfaces
{
    public interface IAppointmentService
    {
        public List<Appointments> GetAppointments();
        public List<Appointments> GetDoctorsAppointments(int userId);
        public List<Appointments> GetBookedAppointmentsForDoctorForTimeFrame(GetDoctorAppointmentsRequest request);
        public bool CreateAppointment(CreateAppointmentRequest request);
        public bool UpdateAppointmentTime(UpdateAppointmentRequest request);
        public bool DeleteAppointment(int appointmentId, int userId);
    }
}
