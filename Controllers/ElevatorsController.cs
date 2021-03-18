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
        //Declare context attribute
        private readonly rocketelevators_developmentContext _context;

        //Constructor
        public ElevatorsController(rocketelevators_developmentContext context)
        {
            _context = context;
        }


        // GET api/elevators
        [HttpGet("status/{status}")]
        // User is free to check different status : in our case just make intervention 
        public IEnumerable<Elevators> GetIntervention(string status)
        {
            //Prepare the request 
            IQueryable<Elevators> elevators = from l in _context.Elevators
            //define condition status should be equal to given values 
                                             where l.Status == status
                                             select l;
            //show results 
            return elevators.ToList();

        }

        // GET api/elevators/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevators>> Getelevators(long id)
        {
            //Get the elevator having specified id 
            var elevator = await _context.Elevators.FindAsync(id);
            //check if no elevetor is returned 
            if (elevator == null)
            {
                return NotFound();
            }

            return elevator;
        }
        
        // PUT api/elevators/id
        // Request to change elevator status 
         [HttpPut("{id}")]
        public async Task<IActionResult> PutmodifyElevatorsStatus(long id, [FromBody] Elevators body)
        {
            //check body 
            if (body.Status == null)
                return BadRequest();
            //find corresponding elevator 
            var elevator = await _context.Elevators.FindAsync(id);
            //change status 
            elevator.Status = body.Status;          
            try
            {
                //save change 
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //catch error - elevetor doesn't exist 
                if (!elevatorExists(id))
                    return NotFound();
                else
                    throw;
            }
            //return succeed message 
            return new OkObjectResult("success");
        }

        private bool elevatorExists(long id)
        {
            return _context.Elevators.Any(e => e.Id == id);
        }

    }
}