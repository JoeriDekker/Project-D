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
        private readonly IWaterLevelSettings _WaterLevelSettingsService;
        private readonly IRepository<User> _userRepository;

        public WaterLevelSettingsController(IRepository<WaterLevelSettings> waterlevelsettingsRepository, IRepository<User> userRepository, IWaterLevelSettings waterLevelSettingsService)
        {
            _waterlevelsettingsRepository = waterlevelsettingsRepository;
            _userRepository = userRepository;
            _WaterLevelSettingsService = waterLevelSettingsService;
        }
        

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] WaterLevelSettingsPatchBody waterlevelsettingsPatchBody)
        {
            Console.WriteLine("WaterLevelSettingsController Put");
            Console.WriteLine("-----------------------------");
            Console.WriteLine(waterlevelsettingsPatchBody);

            const string unauthorizedmsg = "Errors.unauth";

            if (waterlevelsettingsPatchBody == null || (waterlevelsettingsPatchBody.PoleHeight == "" && waterlevelsettingsPatchBody.IdealHeight == ""))
            {
                return BadRequest(new ErrorBody("Missing fields, incident reported."));
            }

            WaterLevelSettingsPatchBodyDecimal checkedSettings;

            Console.WriteLine("Check 1");

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

            Console.WriteLine("Check 2");

            string userId = currentUser.Claims.FirstOrDefault(c => c.Type == "Id")!.Value;
            if (Guid.TryParse(userId, out Guid id) == false)
            {
                return Unauthorized(new ErrorBody(unauthorizedmsg));
            }

            Console.WriteLine("Check 3.1");

            var user = _userRepository.Get(id);
            if (user == null)
            {
                return Unauthorized(new ErrorBody(unauthorizedmsg));
            }
            
            Console.WriteLine("Check 3.2");
            Console.WriteLine(user.Id);

            var existingSettings = _WaterLevelSettingsService.GetByUserId(user.Id);
            if (existingSettings == null)
            {
                return NotFound(new ErrorBody("Settings not found."));
            }

            Console.WriteLine("Check 4");

            


            // Update the settings with provided values, preserving existing ones if not provided.
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
