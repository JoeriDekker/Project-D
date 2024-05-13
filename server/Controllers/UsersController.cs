using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WAMServer.DTO;
using WAMServer.Interfaces;
using WAMServer.Models;

namespace WAMServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Address> _addressRepository;
        public UsersController(IUserRepository userRepository, IRepository<Address> addressRepo)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepo;
        }

        // TODO: Remove me!
        /// <summary>
        /// Did something, felt cute, might delete later
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<UserDTO> GetUser()
        {
            var currentUser = HttpContext.User;
            if (!currentUser.HasClaim(c => c.Type == "Id"))
            {
                return Unauthorized();
            }
            string userId = currentUser.Claims.FirstOrDefault(c => c.Type == "Id")!.Value;
            User? user = _userRepository.GetUserIncludingAddress(_userRepository.GetUser(Guid.Parse(userId)));
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new UserDTO(user));
        }

    }
}
