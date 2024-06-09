using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAMServer.Interfaces;
using WAMServer.Models;
using WAMServer.Records.Bodies;

namespace WAMServer.Controllers
{
    /// <summary>
    /// The ControlPC controller. Responsible for managing ControlPC records.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ControlPCController : ControllerBase
    {
        private readonly IRepository<ControlPC> _controlPCRepository;
        private readonly IRepository<User> _userRepository;

        private readonly IControlPC<ControlPC> _controlPCService;

        /// <summary>
        /// The constructor of the ControlPC controller.
        /// </summary>
        /// <param name="controlPCRepository">The ControlPC repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public ControlPCController(IRepository<ControlPC> controlPCRepository, IRepository<User> userRepository, IControlPC<ControlPC> controlPCService)
        {
            _controlPCRepository = controlPCRepository;
            _userRepository = userRepository;
            _controlPCService = controlPCService;
        }

        /// <summary>
        /// Gets all ControlPC records.
        /// </summary>
        /// <returns>The list of ControlPC records.</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ControlPC>>> GetControlPCs()
        {
            var controlPCs = await _controlPCRepository.GetAllAsync();
            return Ok(controlPCs);
        }

        /// <summary>
        /// Gets a ControlPC record by its ID.
        /// </summary>
        /// <param name="id">The ID of the ControlPC record.</param>
        /// <returns>The ControlPC record.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ControlPC>> GetControlPC(Guid id)
        {
            var controlPC = await _controlPCService.GetAsync(id);
            if (controlPC == null)
            {
                return NotFound();
            }
            return Ok(controlPC);
        }

        /// <summary>
        /// Creates a new ControlPC record.
        /// </summary>
        /// <param name="controlPC">The ControlPC object.</param>
        /// <returns>The created ControlPC record.</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ControlPC>> CreateControlPC([FromBody] ControlPC controlPC)
        {
            if (controlPC == null)
            {
                return BadRequest(new { message = "Invalid data." });
            }

            var currentUser = HttpContext.User;
            if (!currentUser.HasClaim(c => c.Type == "Id"))
            {
                return Unauthorized(new { message = "Unauthorized." });
            }

            string userId = currentUser.Claims.FirstOrDefault(c => c.Type == "Id")!.Value;
            if (!Guid.TryParse(userId, out Guid id))
            {
                return Unauthorized(new { message = "Invalid user ID." });
            }

            controlPC.userId = id;
            controlPC.Id = Guid.NewGuid();
            try
            {
                await _controlPCService.CreateAsync(controlPC);
            } catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
            return CreatedAtAction(nameof(GetControlPC), new { id = controlPC.Id }, controlPC);
        }

        /// <summary>
        /// Updates an existing ControlPC record.
        /// </summary>
        /// <param name="id">The ID of the ControlPC record.</param>
        /// <param name="controlPC">The ControlPC object.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateControlPC(Guid id, [FromBody] ControlPC controlPC)
        {
            if (controlPC == null || id != controlPC.Id)
            {
                return BadRequest(new { message = "Data mismatch." });
            }

            var existingControlPC = await _controlPCService.GetAsync(id);
            if (existingControlPC == null)
            {
                return NotFound();
            }

            try
            {
                await _controlPCRepository.UpdateAsync(controlPC, _ => _.Id == id);
            } catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes a ControlPC record by its ID.
        /// </summary>
        /// <param name="id">The ID of the ControlPC record.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteControlPC(Guid id)
        {
            var controlPC = await _controlPCService.GetAsync(id);
            if (controlPC == null)
            {
                return NotFound();
            }

            await _controlPCRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}