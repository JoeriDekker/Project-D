using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WAMServer.Interfaces;
using WAMServer.Models;
using WAMServer.Records.Bodies;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Put([FromBody] WaterLevelSettingsPatchBody waterlevelsettingsPatchBody)
        {
            const string unauthorizedmsg = "Errors.unauth";

            if (waterlevelsettingsPatchBody == null || (waterlevelsettingsPatchBody.PoleHeight == "" && waterlevelsettingsPatchBody.IdealHeight == ""))
            {
                return BadRequest(new ErrorBody("Missing fields, incident reported."));
            }

            WaterLevelSettingsPatchBodyDecimal checkedSettings;

            if (decimal.TryParse(waterlevelsettingsPatchBody.PoleHeight, out decimal poleDecimal) && decimal.TryParse(waterlevelsettingsPatchBody.IdealHeight, out decimal idealDecimal))
            {
                checkedSettings = new WaterLevelSettingsPatchBodyDecimal
                {
                    PoleHeight = poleDecimal,
                    IdealHeight = idealDecimal
                };            
            }
            else
            {
                return NotFound(new ErrorBody("Decimals not not found."));
            }            

            var currentUser = HttpContext.User;
            if (!currentUser.HasClaim(c => c.Type == "Id"))
            {
                return Unauthorized(new ErrorBody(unauthorizedmsg));
            }

            string userId = currentUser.Claims.FirstOrDefault(c => c.Type == "Id")!.Value;
            if (Guid.TryParse(userId, out Guid id) == false)
            {
                return Unauthorized(new ErrorBody(unauthorizedmsg));
            }

            var user = _userRepository.Get(id);
            if (user == null)
            {
                return Unauthorized(new ErrorBody(unauthorizedmsg));
            }

            // cant find settings
            var existingSettings = _waterlevelsettingsRepository.Get(user.Id);
            if (existingSettings == null)
            {
                return NotFound(new ErrorBody("Settings not found."));
            }

            var updatedSettings = new WaterLevelSettingsPatchBodyDecimal
            {
                PoleHeight = checkedSettings.PoleHeight ?? existingSettings.PoleHeight,
                IdealHeight = checkedSettings.IdealHeight ?? existingSettings.IdealHeight
            };

            await _waterlevelsettingsRepository.UpdateAsync(new WaterLevelSettings(updatedSettings), _ => _.UserId == id);
            return Ok();
        }
    }
}
