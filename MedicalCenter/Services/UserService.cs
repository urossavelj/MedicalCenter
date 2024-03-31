using MedicalCenter.Db;
using MedicalCenter.Db.Models;
using MedicalCenter.Interfaces;
using MedicalCenter.Requests;

namespace MedicalCenter.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public List<User> GetUsers()
        {
            using (var db = new Context())
            {
                var users = db.Users.ToList();
                return users;
            }
        }

        public bool CreateUser(CreateUserRequest request)
        {
            using (var db = new Context())
            {
                var user = new User
                {
                    Username = request.Username,
                    Password = request.Password,
                    UserType = request.UserType,
                };

                db.Users.Add(user);
                var response = db.SaveChanges();

                if (response == 1)
                    return true;
                else
                {
                    _logger.LogError("An error occured when creating a user");
                    return false;
                }
            }
        }
    }
}
