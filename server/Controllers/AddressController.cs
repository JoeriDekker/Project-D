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

        public AddressController(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpPatch]
        [Authorize]
        public IActionResult Patch([FromBody] AddressPatchBody addressPatchBody)
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
            _addressRepository.UpdateAsync(new Address(addressPatchBody), _ => _.UserId == id);
            return Ok();
        }
    }
}