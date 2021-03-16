using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket_Elevators_Rest_API.Models;


namespace Rocket_Elevators_Rest_API.Controllers
{
    [Route("api/[controller]")]
    public class ElevatorsController : ControllerBase
    {
        public ElevatorsController(AppDb db)
        {
            Db = db;
        }

        // GET api/elevators
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetIdle(string status)
        {
            await Db.Connection.OpenAsync();
            var query = new ElevatorsQuery(Db);
            var result = await query.IdleElevatorsAsync(status);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // GET api/elevators/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new ElevatorsQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }
        // PUT api/elevators/id
        [HttpPost("{id}")]
        public async Task<IActionResult> PostOne(int id, [FromBody] Elevators body)
        {
            await Db.Connection.OpenAsync();
            var query = new ElevatorsQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();            

            result.Status = body.Status;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        public AppDb Db { get; }
    }
}