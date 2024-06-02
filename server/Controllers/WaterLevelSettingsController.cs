using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WAMServer.Interfaces;
using WAMServer.Models;
using WAMServer.Records.Bodies;
using System;
using System.Linq;

namespace WAMServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterLevelSettingsController : ControllerBase
    {
        private readonly IRepository<WaterLevelSettings> _waterlevelsettingsRepository;
        private readonly IRepository<User> _userRepository;

        public WaterLevelSettingsController(IRepository<WaterLevelSettings> waterlevelsettingsRepository, IRepository<User> userRepository)
        {
            _waterlevelsettingsRepository = waterlevelsettingsRepository;
            _userRepository = userRepository;
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] WaterLevelSettingsPatchBody waterlevelsettingsPatchBody)
        {
            const string unauthorizedmsg = "Errors.unauth";

            // Check if the body is null or if both fields are null. If so, return a bad request.
            if (waterlevelsettingsPatchBody == null || (waterlevelsettingsPatchBody.PoleHeight == null && waterlevelsettingsPatchBody.IdealHeight == null))
            {
                return BadRequest(new ErrorBody("Missing fields, incident reported."));
            }

            var currentUser = HttpContext.User;
            if (!currentUser.HasClaim(c => c.Type == "Id"))
            {
                return Unauthorized(new ErrorBody(unauthorizedmsg));
            }
            string userId = currentUser.Claims.FirstOrDefault(c => c.Type == "Id")!.Value;
            if (!Guid.TryParse(userId, out Guid id))
            {
                return Unauthorized(new ErrorBody(unauthorizedmsg));
            }

            var user = _userRepository.Get(id);
            if (user == null)
            {
                return Unauthorized(new ErrorBody(unauthorizedmsg));
            }

            var existingSettings = _waterlevelsettingsRepository.Get(user.Id);
            if (existingSettings == null)
            {
                return NotFound(new ErrorBody("Settings not found."));
            }

            // Update the settings with provided values, preserving existing ones if not provided.
            if (waterlevelsettingsPatchBody.PoleHeight != null)
            {
                existingSettings.PoleHeight = waterlevelsettingsPatchBody.PoleHeight;
            }

            if (waterlevelsettingsPatchBody.IdealHeight != null)
            {
                existingSettings.IdealHeight = waterlevelsettingsPatchBody.IdealHeight;
            }

            _waterlevelsettingsRepository.UpdateAsync(existingSettings, _ => _.UserId == id);
            return Ok();
        }
    }
}
