using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.AuthManager;
using UserManagement.InputModels;

namespace UserManagement.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IJWTAuthenticationManager jwtAuthenticationManager;

        public LoginController(IJWTAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] UserCredential userCreds)
        {
            string token = jwtAuthenticationManager.Authenticate(userCreds.UserName, userCreds.Password);

            if (token == null)
                return Unauthorized();

            return Ok(new { token });
        }

        [Route("register")]
        [HttpPost]
        public IActionResult Register([FromBody] UserRegistration userRegistration)
        {
            return Ok();
        }
    }
}
