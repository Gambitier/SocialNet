using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.AuthManager;
using UserManagement.InputModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IJWTAuthenticationManager jwtAuthenticationManager;

        public LoginController(IJWTAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        // POST api/<AuthorizationController>
        [HttpPost]
        public IActionResult Login([FromBody] UserCredential userCreds)
        {
            string token = jwtAuthenticationManager.Authenticate(userCreds.UserName, userCreds.Password);

            if (token == null)
                return Unauthorized();

            return Ok(new { token });
        }
    }
}
