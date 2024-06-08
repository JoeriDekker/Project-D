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
    /// Controller responsible for handling water storage-related operations.
    /// </summary>

    [Route("/api/[controller]")]
    [ApiController]
    public class WaterStorageController : ControllerBase{

        public readonly IRepository<WaterStorage> _waterStorageRepository;

        public readonly IRepository<User> _userRepository;

        public readonly IRepository<ControlPC> _controlPCRepository;

        /// <summary>
        /// Constructor for the WaterStorageController.
        /// </summary>
        /// <param name="waterStorageRepository">The WaterStorage repository</param>
        public WaterStorageController(IRepository<WaterStorage> waterStorageRepository, IRepository<User> userRepository, IRepository<ControlPC> controlPCRepository)
        {
            _waterStorageRepository = waterStorageRepository;
            _userRepository = userRepository;
            _controlPCRepository = controlPCRepository;

        }

        /// <summary>
        /// Gets all water storages.
        /// </summary>
        /// <returns>The list of WaterStorage records</returns>
        // [HttpGet]
        // [Authorize]
        // public async Task<ActionResult<IEnumerable<WaterStorage>>> GetWaterStorages()
        // {
        //     var waterstorages = await _waterStorageRepository.GetAllAsync();
        //     return Ok(waterstorages);
        // }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<WaterStorage> GetWaterStorageFromID(Guid id)
        {
            // Get all Waterstoreges from the user with the given id in getall() method


            var controlPC = _controlPCRepository.GetAll(c => c.userId == id).FirstOrDefault();
            
            if (controlPC == null)
            {
                return NotFound();
            }

            var waterstorages = _waterStorageRepository.GetAll(w => w.ControlPCID == controlPC.Id);

            if (waterstorages == null)
            {
                return NotFound();
            }
            
            return Ok(waterstorages);

        }

    }

}