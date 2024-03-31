namespace MedicalCenter.Db.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime WorkingTimeFrom { get; set; }
        public DateTime WorkingTimeTo { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
