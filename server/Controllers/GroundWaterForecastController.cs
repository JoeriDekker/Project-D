using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WAMServer.Interfaces;
using WAMServer.Models;

namespace WAMServer.Controllers
{
    public class GroundWaterForecastController : ControllerBase
    {

        private readonly IRepository<User> _userRepository;

        public GroundWaterForecastController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Returns the forecasted ground waterlevel for tomorrow.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult GetGroundWaterForecast()
        {
            var currentUser = HttpContext.User;
            if (!currentUser.HasClaim(c => c.Type == "Id"))
            {
                return Unauthorized();
            }
            string userId = currentUser.Claims.FirstOrDefault(c => c.Type == "Id")!.Value;
            if (Guid.TryParse(userId, out Guid id) == false)
            {
                return Unauthorized();
            }

            var user = _userRepository.Get(id);
            if (user == null)
            {
                return Unauthorized();
            }

            // Get the house area of the user or default to 125
            decimal houseArea = user.ControlPC?.HouseArea ?? 125;

            // TODO: Implement the forecast logic
            

            return Ok();
        }
    }
}