namespace MedicalCenter.Requests
{
    public class UpdateAppointmentRequest
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public DateTime BookingTime { get; set; }
    }
}
