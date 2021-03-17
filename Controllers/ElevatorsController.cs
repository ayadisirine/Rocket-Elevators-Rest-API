using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket_Elevators_Rest_API.Models;
using Rocket_Elevators_Rest_API.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Rocket_Elevators_Rest_API.Controllers
{
    [Route("api/[controller]")]
    public class ElevatorsController : ControllerBase
    {

        private readonly rocketelevators_developmentContext _context;

        public ElevatorsController(rocketelevators_developmentContext context)
        {
            _context = context;
        }


        // GET api/elevators
        [HttpGet("status/{status}")]
        public async Task<ActionResult<Elevators>> GetIntervention(string status)
        {
            var elevator = await _context.Elevators.FindAsync(status);

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator;
        }

        // GET api/elevators/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevators>> Getelevators(long id)
        {
            var elevator = await _context.Elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator;
        }
        // PUT api/elevators/id
     [HttpPut("{id}")]
        public async Task<IActionResult> PutmodifyElevatorsStatus(long id, [FromBody] Elevators body)
        {



            if (body.Status == null)
                return BadRequest();

            var elevator = await _context.Elevators.FindAsync(id);
            elevator.Status = body.Status;          
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!elevatorExists(id))
                    return NotFound();
                else
                    throw;
            }

            return new OkObjectResult("success");
        }

        private bool elevatorExists(long id)
        {
            return _context.Elevators.Any(e => e.Id == id);
        }

    }
}