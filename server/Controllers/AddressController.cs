using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WAMServer.Interfaces;
using WAMServer.Models;
using WAMServer.Records.Bodies;

namespace WAMServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<User> _userRepository;

        public AddressController(IRepository<Address> addressRepository, IRepository<User> userRepository)
        {
            _addressRepository = addressRepository;
            _userRepository = userRepository;
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] AddressPatchBody addressPatchBody)
        {
            const string passincorrectmsg = "Errors.incorrectpassword";
            const string unauthorizedmsg = "Errors.unauth";

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

            if (!BCrypt.Net.BCrypt.EnhancedVerify(addressPatchBody.Password, user.Password))
            {
                return Unauthorized(new ErrorBody(passincorrectmsg));
            }

            _addressRepository.UpdateAsync(new Address(addressPatchBody), _ => _.UserId == id);
            return NoContent();
        }
    }
}