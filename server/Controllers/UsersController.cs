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
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Address> _addressRepository;
        public UsersController(IRepository<User> userRepository, IRepository<Address> addressRepo)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepo;
        }


        [HttpGet]
        [Authorize]
        public ActionResult<UserDTO> GetUser()
        {
            var currentUser = HttpContext.User;
            if (!currentUser.HasClaim(c => c.Type == "Id"))
            {
                return Unauthorized();
            }
            string userId = currentUser.Claims.FirstOrDefault(c => c.Type == "Id")!.Value;
            if (!Guid.TryParse(userId, out Guid id))
            {
                return Unauthorized();
            }
            User? user = _userRepository.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            // If the user has an address, get it from the address repository
            if (user.AddressId != null)
            {
                user.Address = _addressRepository.Get(user.AddressId.Value);
            }
            return Ok(new UserDTO(user));
        }

    }
}
