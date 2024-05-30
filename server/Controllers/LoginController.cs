using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WAMServer.Interfaces;
using WAMServer.Models;
using WAMServer.Records;
using WAMServer.Records.Bodies;
using WAMServer.Validation;

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
        private ILoginService loginService;

        /// <summary>
        /// The constructor of the login controller.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="config">The configuration of the application.</param>
        public LoginController(ILoginService loginService, IConfiguration config)
        {
            _config = config;
            this.loginService = loginService;
        }

        /// <summary>
        /// Logs in the user.
        /// </summary>
        /// <param name="body">The login body. Requires email and password</param>
        /// <returns>The result of the login.</returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] LoginBody body)
        {
            const string errormsg = "The email address and/or the password did not match.";
            ActionResult response = Unauthorized(errormsg);
            if (!InputValidation.IsValidEmail(body.Email))
            {
                return response;
            }
            // Authenticate the user. If the user is authenticated, generate a token. Otherwise (user is null), return Unauthorized.
            var user = AuthenticateUserReturnNullIfUnable(body.Email, body.Password);

            if (user != null && user.IsConfirmed)
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
        [NonAction]
        public string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "ThisIsTheWamsSuperSecretKey782623"));
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
        [NonAction]
        public User? AuthenticateUserReturnNullIfUnable(string email, string password)
        {
            User? user = null;
            User? userQueryResult = loginService.GetUser(email);
            if (userQueryResult == null)
            {
                return user;
            }
            if (BCrypt.Net.BCrypt.EnhancedVerify(password, userQueryResult?.PasswordHash ?? ""))
            {
                user = userQueryResult;
            }
            
            return user;
        }
    }
}