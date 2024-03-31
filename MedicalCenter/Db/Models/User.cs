using MedicalCenter.Enums;

namespace MedicalCenter.Db.Models
{
    public class User
    {
        public int Id { get; set; }
        public UserType UserType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
