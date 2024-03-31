namespace MedicalCenter.Requests
{
    public class CreateAppointmentRequest
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime BookingTime { get; set; }
    }
}
