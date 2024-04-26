using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WAMServer.Interfaces;
using WAMServer.Models;
using WAMServer.Records;

namespace WAMServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository, IConfiguration config)
        {
            _config = config;
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginBody body)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(body.Email, body.Password);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "WamsSuperSecretKey"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim("Id", userInfo.Id.ToString()),
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User? AuthenticateUser(string email, string password)
        {
            User? user = null;
            //TODO: Add encryption
            User? userQueryResult = _userRepository.GetUser(email);
            if (userQueryResult?.Password == password)
            {
                user = userQueryResult;
            }
            
            return user;
        }
    }
}