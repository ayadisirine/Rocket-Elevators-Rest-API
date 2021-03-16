using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket_Elevators_Rest_API.Models;
using Microsoft.EntityFrameworkCore;
using Rocket_Elevators_Rest_API.Data;

using Pomelo.EntityFrameworkCore.MySql;

namespace Rocket_Elevators_Rest_API.Models.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class columnsController : ControllerBase
    {
        private readonly rocketelevators_developmentContext _context;

        public columnsController(rocketelevators_developmentContext context)
        {
            _context = context;
        }

        //Action that gives the list of all columns
        // GET: api/columns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Columns>>> Getcolumns()
        {
            return await _context.Columns.ToListAsync();
        }

         // Action that recuperates a given column 
        // GET: api/columns/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Columns>> Getcolumns(long id)
        {
            var column = await _context.Columns.FindAsync(id);

            if (column == null)
            {
                return NotFound();
            }

            return column;
        }

       
        //Action that recuperates the status of a given column
        [HttpGet("{id}/status")]
        public async Task<ActionResult<string>> GetcolumnStatus(long id)
        {
            var columns = await _context.Columns.FindAsync(id);

            if (columns == null)
            {
                return NotFound();
            }

            return columns.Status;
            
        }

        
        
        // PUT: api/columns/id/updatestatus        
        [HttpPut("{id}/updatestatus")]
        public async Task<IActionResult> PutmodifyColumnStatus(long id, string Status)
        {
            if (Status == null)
            {
                return BadRequest();
            }

            var column = await _context.Columns.FindAsync(id);

            column.Status = Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!columnsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool columnsExists(long id)
        {
            return _context.Columns.Any(e => e.Id == id);
        }
    }
}
