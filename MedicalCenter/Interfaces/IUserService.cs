using MedicalCenter.Db.Models;
using MedicalCenter.Requests;

namespace MedicalCenter.Interfaces
{
    public interface IUserService
    {
        public List<User> GetUsers();
        public bool CreateUser(CreateUserRequest request);
    }
}
