using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using WAMServer.Interfaces;
using WAMServer.Models;
using WAMServer.Records.Bodies;
using WAMServer.Validation;

namespace WAMServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        private IRepository<User> _userRepository;

        public RegisterController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Post([FromBody] UserBody body)
        {
            var usersWithEmail = _userRepository.GetAll(user => user.Email == body.Email);
            if (usersWithEmail.Count() > 0)
            {
                return BadRequest(new ErrorBody("Register.emailtaken"));
            }
            if (!InputValidation.IsValidEmail(body.Email))
            {
                return BadRequest(new ErrorBody("Wrong approach, incident reported."));
            }
            if (!InputValidation.IsValidPassword(body.Password))
            {
                return BadRequest(new ErrorBody("Wrong approach, incident reported."));
            }
            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(body.Password);
            var user = new User(body.FirstName, body.LastName, body.Email, hashedPassword);
            return await _userRepository.AddAsync(user);
        }

        [HttpGet("confirm")]
        [AllowAnonymous]
        public ActionResult Confirm([FromQuery] string userId, [FromQuery] string token)
        {
            var user = _userRepository.Get(Guid.Parse(userId));
            if (user == null)
            {
                return NotFound();
            }
            
            return Ok();
        }
        

    }
}