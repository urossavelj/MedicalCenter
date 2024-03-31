namespace MedicalCenter.Requests
{
    public class DeleteAppointmentRequest
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
    }
}
