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

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetUser()
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
