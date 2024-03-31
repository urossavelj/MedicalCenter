using MedicalCenter.Enums;

namespace MedicalCenter.Requests
{
    public class AuthenticateUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
