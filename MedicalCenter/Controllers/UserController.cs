using MedicalCenter.Interfaces;
using MedicalCenter.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCenter.Controllers
{
    [ApiController]
    [Authorize(Policy = "Bearer")]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Lists all users
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public IActionResult CreateUser(CreateUserRequest request)
        {
            var userCreated = _userService.CreateUser(request);

            if (userCreated)
                return Ok();
            else
                return BadRequest();
        }

        /// <summary>
        /// Authenticates user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate(AuthenticateUserRequest request)
        {
            var token = _authenticationService.Authenticate(request);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
