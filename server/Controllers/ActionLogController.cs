using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAMServer.Interfaces;
using WAMServer.Models;
using WAMServer.Records.Bodies;

namespace WAMServer.Controllers{
    /// <summary>
    /// Controller responsible for handling action logs operations.
    /// </summary>
    
    [Route("/api/[controller]")]
    [ApiController]
    public class ActionLogController : ControllerBase{

        public readonly IRepository<ActionLog> _actionRepository;

        /// <summary>
        /// Constructor for the ActionLogController.
        /// </summary>
        /// <param name="actionLogRepository">The ActionLog repository</param>
        public ActionLogController(IRepository<ActionLog> actionRepository)
        {
            _actionRepository = actionRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ActionLog>>> GetActionLogs()
        {
            var actionlogs = await _actionRepository.GetAllAsync();
            Console.WriteLine(actionlogs);
            return Ok(actionlogs);
        }

    }
}