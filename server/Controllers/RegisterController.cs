using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private IRepository<Address> _addressRepository;
        private IRepository<WaterLevelSettings> _waterLevelSettingsRepository;
        private IEmailService _mailService;
        private IConfiguration _configuration;

        public RegisterController(IRepository<User> userRepository, IEmailService mailService, IConfiguration configuration, IRepository<Address> addressRepository, IRepository<WaterLevelSettings> waterLevelSettingsRepository)
        {
            _userRepository = userRepository;
            _mailService = mailService;
            _configuration = configuration;
            _addressRepository = addressRepository;
            _waterLevelSettingsRepository = waterLevelSettingsRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] UserBody body)
        {
            string? backendURL = _configuration["JWT:Issuer"];
            string? frontendURL = _configuration["FrontendURL"];
            if(backendURL == null || frontendURL == null)
            {
                return BadRequest(new ErrorBody("Register.failed"));
            }

            // Check if the email is already taken
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
            var address = new Address
            {
                Street = "",
                City = "",
                Zip = "",
                HouseNumber = "",
                UserId = user.Id
            };
            User registeredUser;
            WaterLevelSettings waterLevelSettings;
            try
            {
                registeredUser = await _userRepository.AddAsync(user);
                waterLevelSettings = await _waterLevelSettingsRepository.AddAsync(
                new WaterLevelSettings()
                {
                    UserId = user.Id,
                    IdealHeight = -1.85m,
                    PoleHeight = -2.05m
                }
            );
            await _addressRepository.AddAsync(address);
            } catch (Exception)
            {
                return BadRequest(new ErrorBody("Register.failed"));
            }
            
            user.AddressId = address.Id;
            user.WaterLevelSettingsId = waterLevelSettings.Id;
            await _userRepository.UpdateAsync(user, u => u.Id == user.Id);
            _mailService.SendEmail(user.Email, "Welcome to WAM", $"We are pleased to hear you want to join the fight against polerot. In order to log in, please confirm your email address by clicking the following link: {frontendURL}/verify/" + user.Id + "/" + user.ConfirmationToken);
            return Ok();
        }

        [HttpGet("confirm")]
        [AllowAnonymous]
        public async Task<ActionResult> Confirm([FromQuery] string userId, [FromQuery] string token)
        {
            string? frontendURL = _configuration["FrontendURL"];
            if(frontendURL == null)
            {
                return BadRequest();
            }
            User? user;
            try {
            user = _userRepository.Get(Guid.Parse(userId));
            } catch (FormatException)
            {
                return BadRequest();
            }
            if (user == null)
            {
                return NotFound();
            }
            if (user.ConfirmationToken.ToString() != token)
            {
                return BadRequest();
            }
            user.IsConfirmed = true;
            await _userRepository.UpdateAsync(user, u => u.Id == user.Id);
            return Ok(frontendURL);
        }
        

    }
}