using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.AuthManager;
using UserManagement.RequestModels;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IJWTAuthenticationManager _jwtAuthenticationManager;

        private readonly IUserServices _userServices;

        public UsersController(
            IJWTAuthenticationManager jwtAuthenticationManager,
            IUserServices userServices)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _userServices = userServices;
        }

        [HttpGet]
        public IActionResult GetAllRegisteredUsers()
        {
            List<DataModels.User> users = _userServices.GetAllRegisteredUsers();
            return Ok(users);
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] UserCredential userCreds)
        {
            string token = _jwtAuthenticationManager.Authenticate(userCreds);

            if (token == null)
                return Unauthorized();

            return Ok(new { token });
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignupAsync([FromBody] UserRegistration userRegistration)
        {
            var userId = await _userServices.RegisterUserAsync(userRegistration);
            return Ok(new { userId });
        }
    }
}
