using MedicalCenter.Enums;

namespace MedicalCenter.Requests
{
    public class CreateUserRequest
    {
        public UserType UserType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
