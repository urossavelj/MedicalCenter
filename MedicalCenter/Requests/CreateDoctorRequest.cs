namespace MedicalCenter.Requests
{
    public class CreateDoctorRequest
    {
        public string Name { get; set; }
        public DateTime WorkingTimeFrom { get; set; }
        public DateTime WorkingTimeTo { get; set; }
        public int UserId { get; set; }
    }
}
