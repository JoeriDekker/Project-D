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
    /// Controller responsible for handling groundwater log-related operations.
    /// </summary>
    
    [Route("/api/[controller]")]
    [ApiController]
    public class GroundWaterLogController : ControllerBase{

        public readonly IRepository<GroundWaterLog> _groundWaterLogRepository;

        /// <summary>
        /// Constructor for the GroundWaterLogController.
        /// </summary>
        /// <param name="groundWaterLogRepository">The GroundWaterLog repository</param>
        public GroundWaterLogController(IRepository<GroundWaterLog> groundWaterLogRepository)
        {
            _groundWaterLogRepository = groundWaterLogRepository;
        }

        /// <summary>
        /// Gets all groundwater logs.
        /// </summary>
        /// <returns>The list of Groundwaterlog records</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GroundWaterLog>>> GetGroundWaterLogs()
        {
            var groundwaterlogs = await _groundWaterLogRepository.GetAllAsync();
            Console.WriteLine(groundwaterlogs);
            return Ok(groundwaterlogs);
        }

    }
}