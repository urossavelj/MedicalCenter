namespace MedicalCenter.Requests
{
    public class GetDoctorAppointmentsRequest
    {
        public int DoctorId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
