using MedicalCenter.Db.Models;
using MedicalCenter.Requests;

namespace MedicalCenter.Interfaces;
public interface IAuthenticationService
{
    AuthenticationToken? Authenticate(AuthenticateUserRequest request);
}