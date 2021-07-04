using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public IActionResult Signup([FromBody] UserRegistration userRegistration)
        {
            var userId = _userServices.RegisterUser(userRegistration);
            return Ok(new { userId });
        }
    }
}
