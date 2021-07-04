using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.RequestModels;
using UserManagement.Services;

namespace UserManagement.AuthManager
{
    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {
        private readonly string _key;
        private readonly IUserServices _userServices;

        public JWTAuthenticationManager(IConfiguration Configuration, IUserServices userServices)
        {
            _key = Configuration.GetValue<string>("TokenKey");
            _userServices = userServices;
        }

        public string Authenticate(UserCredential userCreds)
        {
            if(!_userServices.VerifyUserCredentials(userCreds))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userCreds.UserName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
