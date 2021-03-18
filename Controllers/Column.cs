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
      
      
        
        // POST: api/Columns
       
        [HttpPost]
        public async Task<ActionResult<Columns>> PostColumn(Columns column)
        {
            _context.Columns.Add(column);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColumn", new { id = column.Id }, column);
        }

        //Action that recuperates the status of a given column
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Columns>> DeleteColumn(int id)
        {
            var column = await _context.Columns.FindAsync(id);
            if (column == null)
            {
                return NotFound();
            }

            _context.Columns.Remove(column);
            await _context.SaveChangesAsync();

            return column;
        }

        private bool ColumnExists(int id)
        {
            return _context.Columns.Any(e => e.Id == id);
        }
        
       //PUT: api/columns/id/updatestatus        
        [HttpPut("{id}/Status")]
        public async Task<IActionResult> PutmodifyColumnStatus( long id, string Status)
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
                if (!ColumnExists(id))
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
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutColumn(long id, Columns column)
        // {
        //     if (id != column.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(column).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ColumnExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }


        [HttpGet("{id}/status")]
        public async Task<ActionResult<string>> GetColumnStatus([FromRoute] long id)
        {
            var column = await _context.Columns.FindAsync(id);

            if (column == null)
            {
                return NotFound();
            }

            return column.Status;
        }

        private bool ColumnExists(long id)
        {
            return _context.Columns.Any(e => e.Id == id);
        }
    }
}
