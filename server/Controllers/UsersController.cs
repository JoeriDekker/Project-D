using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WAMServer.DTO;
using WAMServer.Interfaces;
using WAMServer.Models;

namespace WAMServer.Controllers
{
    /// <summary>
    /// Controller responsible for handling user-related operations.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<WaterLevelSettings> _waterlevelsettingsRepository;


        public UsersController(IRepository<User> userRepository, IRepository<Address> addressRepo, IRepository<WaterLevelSettings> settingsRepo)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepo;
            _waterlevelsettingsRepository = settingsRepo;
        }

        /// <summary>
        /// Retrieves the current user's information.
        /// </summary>
        /// <returns>An ActionResult containing the current user's information if authorized; otherwise, returns an UnauthorizedResult.</returns>
        [HttpGet]
        [Authorize]
        public ActionResult<UserDTO> GetUser()
        {
            var currentUser = HttpContext.User;

            // Check if the user has the required claim
            if (!currentUser.HasClaim(c => c.Type == "Id"))
            {
                return Unauthorized();
            }

            // Extract the user id from the claim
            string userId = currentUser.Claims.FirstOrDefault(c => c.Type == "Id")!.Value;

            // Validate the user id
            if (!Guid.TryParse(userId, out Guid id))
            {
                return Unauthorized();
            }

            // Retrieve the user from the repository
            User? user = _userRepository.Get(id);

            // If user not found, return NotFound
            if (user == null)
            {
                return NotFound();
            }

            // If the user has an associated address, retrieve it from the address repository
            if (user.AddressId != null)
            {
                user.Address = _addressRepository.Get(user.AddressId.Value);
                user.WaterLevelSettings = _waterlevelsettingsRepository.Get(user.Id);

            }

            // Return the user DTO as OK response
            return Ok(new UserDTO(user));
        }
    }
}
