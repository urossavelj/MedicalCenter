namespace MedicalCenter.Db.Models
{
    public class Appointments
    {
        public int Id { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
    }
}
