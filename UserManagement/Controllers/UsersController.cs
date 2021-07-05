using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.AuthManager;
using UserManagement.RequestModels;
using UserManagement.ResponseModels;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [Authorize]
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

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(string id)
        {
            UserDto user = await _userServices.GetUserAsync(id);
            return Ok(user);
        }

        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserCredential userCreds)
        {
            string token = await _jwtAuthenticationManager.AuthenticateAsync(userCreds);

            if (token == null) {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(new { token });
        }

        [Route("signup")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignupAsync([FromBody] UserRegistration userRegistration)
        {
            var userId = await _userServices.RegisterUserAsync(userRegistration);
            return Ok(new { userId });
        }
    }
}
