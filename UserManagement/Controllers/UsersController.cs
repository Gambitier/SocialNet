using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IEmailSender _emailSender;

        public UsersController(
            IJWTAuthenticationManager jwtAuthenticationManager,
            IUserServices userServices,
            IEmailSender emailSender)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _userServices = userServices;
            _emailSender = emailSender;
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
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var userId = await _userServices.RegisterUserAsync(userRegistration);

            await _emailSender.SendEmailAsync(
                userRegistration.Email,
                "welcome to usermanagement",
                $"Welcome!! <br/><br/> " +
                $"your username is \"{userRegistration.UserName.Trim().ToLower()}\" " +
                $"and password is \"{userRegistration.Password}\"");

            return Ok(new { userId });
        }
    }
}
