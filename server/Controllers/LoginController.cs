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
    /// <summary>
    /// The login controller. Responsible for logging in the user.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// The configuration of the application.
        /// </summary>
        private IConfiguration _config;
        /// <summary>
        /// The user repository.
        /// </summary>
        private IUserRepository _userRepository;

        /// <summary>
        /// The constructor of the login controller.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="config">The configuration of the application.</param>
        public LoginController(IUserRepository userRepository, IConfiguration config)
        {
            _config = config;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Logs in the user.
        /// </summary>
        /// <param name="body">The login body. Requires email and password</param>
        /// <returns>The result of the login.</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginBody body)
        {
            IActionResult response = Unauthorized();
            // Authenticate the user. If the user is authenticated, generate a token. Otherwise (user is null), return Unauthorized.
            var user = AuthenticateUserReturnNullIfUnable(body.Email, body.Password);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        /// <summary>
        /// Generates a JSON web token.
        /// </summary>
        /// <param name="userInfo">The user information.</param>
        /// <returns>The JSON web token.</returns>
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

        /// <summary>
        /// Authenticates the user. Returns null if unable to authenticate.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The user if authenticated, null otherwise.</returns>
        private User? AuthenticateUserReturnNullIfUnable(string email, string password)
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