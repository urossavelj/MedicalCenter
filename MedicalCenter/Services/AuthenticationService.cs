using MedicalCenter.Db;
using MedicalCenter.Db.Models;
using MedicalCenter.Interfaces;
using MedicalCenter.Requests;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedicalCenter.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;

    public AuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AuthenticationToken? Authenticate(AuthenticateUserRequest request)
    {
        using (var db = new Context())
        {
            var foundUser = db.Users.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);

            if (foundUser == null)
            {
                return null;
            }

            //If user found then generate JWT
            return CreateAuthenticationToken(foundUser);
        }
    }

    private AuthenticationToken CreateAuthenticationToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Role, user.UserType.ToString()),

            }),
            Audience = _configuration["JWT:Audience"],
            Issuer = _configuration["JWT:Issuer"],
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AuthenticationToken()
        {
            Token = tokenHandler.WriteToken(token),
        };
    }
}