using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.AuthManager;
using UserManagement.Services.Services;
using UserManagement.Services.Services.RequestModels;
using UserManagement.Services.Services.ResponseModels;

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
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            string token = await _jwtAuthenticationManager.AuthenticateAsync(userCreds);

            return Ok(new { token });
        }

        [Route("signup")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignupAsync([FromBody] UserRegistration userRegistration)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var userId = await _userServices.RegisterUserAsync(userRegistration);
            return Ok(new { userId });
        }
    }
}
